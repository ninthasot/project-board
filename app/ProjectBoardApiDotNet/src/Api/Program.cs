using Api;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for structured logging
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

var otlpEndpoint = builder.Configuration["OpenTelemetry:OtlpEndpoint"];
if (string.IsNullOrWhiteSpace(otlpEndpoint))
    throw new InvalidOperationException("OpenTelemetry:OtlpEndpoint configuration is missing.");

builder
    .Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(builder.Environment.ApplicationName))
    .WithTracing(tracing =>
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(otlpEndpoint); // Container name and OTLP port
            })
    )
    .WithMetrics(metrics =>
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(otlpEndpoint!);
            })
    );

builder.AddDataBase();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

Log.Information("OpenTelemetry has been initialized and configured.");

Log.Debug("Test OpenTelemetry log");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();
