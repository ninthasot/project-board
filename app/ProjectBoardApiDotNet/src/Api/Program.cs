var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

builder.AddTelemetry().AddExceptionHandlers().AddModuleServices();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
    };
});

builder.Services.AddApplication([Boards.Application.AssemblyReference.Assembly]);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //await app.ApplyMigrationAsync();
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();

//app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();
