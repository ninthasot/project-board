using Api;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataBase();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

await app.RunAsync();
