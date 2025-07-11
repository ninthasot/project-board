using Api;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataBase();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();
