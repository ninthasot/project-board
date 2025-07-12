using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Api;

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddDataBase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ProjectBoardDbContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        DatabaseSchema.ProjectBoard
                    )
            );
        });

        builder.Services.AddDbContext<ProjectIdentityDbContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        DatabaseSchema.Identity
                    )
            );
        });
        return builder;
    }

    public static WebApplicationBuilder AddTelemetry(this WebApplicationBuilder builder)
    {
        var otlpEndpoint = builder.Configuration["OpenTelemetry:OtlpEndpoint"];
        if (string.IsNullOrWhiteSpace(otlpEndpoint))
            throw new InvalidOperationException(
                "OpenTelemetry:OtlpEndpoint configuration is missing."
            );

        builder
            .Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(builder.Environment.ApplicationName))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri(otlpEndpoint);
                        options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                    });
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri(otlpEndpoint);
                        options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                    });
            });
        return builder;
    }
}
