using System.Text.Json;
using Api.Constants;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Api.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<CustomExceptionHandlerMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            ArgumentNullException.ThrowIfNull(context);

            _logger.LogError(
                ex,
                LogMessageConstant.UnhandledException,
                context.Request.Path,
                context.Request.Method,
                context.Response.StatusCode,
                ex.GetType().Name,
                ex.Message
            );

            context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.ProblemJson;

            int status;
            string title;
            string detail;
            var problemDetails = new ProblemDetails { Instance = context.Request.Path };

            switch (ex)
            {
                case PostgresException or TimeoutException:
                    status = 503;
                    title = HttpConstant.ProblemTitleServiceUnavailable;
                    detail = HttpConstant.ProblemDetailTemporarilyUnavailable;
                    break;
                case DbUpdateConcurrencyException:
                    status = 409;
                    title = HttpConstant.ProblemTitleConflict;
                    detail = HttpConstant.ProblemDetailConflict;
                    break;
                case DbUpdateException:
                    status = 500;
                    title = HttpConstant.ProblemTitleInternalServerError;
                    detail = HttpConstant.ProblemDetailDatabaseError;
                    break;
                default:
                    status = 500;
                    title = HttpConstant.ProblemTitleInternalServerError;
                    detail = HttpConstant.ProblemDetailUnexpectedError;
                    break;
            }

            context.Response.StatusCode = status;
            problemDetails.Status = status;
            problemDetails.Title = title;
            problemDetails.Detail = detail;
            problemDetails.Extensions["traceId"] = context.TraceIdentifier;

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}
