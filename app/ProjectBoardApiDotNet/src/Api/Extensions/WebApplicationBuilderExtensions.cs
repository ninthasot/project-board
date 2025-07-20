using Api.ExceptionHandlers;
using Boards.Infrastructure;
using Cards.Infrastructure;
using Labels.Infrastructure;
using Users.Infrastructure;

namespace Api.Extensions;

internal static class WebApplicationBuilderExtensions
{
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

    public static WebApplicationBuilder AddExceptionHandlers(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<TimeoutExceptionHandler>();
        builder.Services.AddExceptionHandler<PostgresExceptionHandler>();
        builder.Services.AddExceptionHandler<DbUpdateExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        return builder;
    }

    public static WebApplicationBuilder AddModuleServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddBoardServices(builder.Configuration);
        builder.Services.AddCardServices(builder.Configuration);
        builder.Services.LabelCardServices(builder.Configuration);
        builder.Services.UserCardServices(builder.Configuration);
        return builder;
    }
}