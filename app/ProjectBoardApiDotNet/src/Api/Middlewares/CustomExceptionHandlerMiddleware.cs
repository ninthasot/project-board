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
            LogException(context, ex);
            context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.ProblemJson;
            string traceId = context.TraceIdentifier;

            switch (ex)
            {
                case ValidationException validationException:
                    var validationProblemDetails =
                        ProblemDetailsBuilder.BuildValidationProblemDetails(
                            validationException.Errors.Select(
                                e => new SharedKernel.Errors.ValidationError(e.ErrorMessage)
                            ),
                            traceId
                        );
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(validationProblemDetails)
                    );
                    break;
                case PostgresException or TimeoutException:
                    await WriteProblemDetailsAsync(
                        context,
                        ProblemDetailsBuilder.BuildProblemDetails(null, traceId),
                        503,
                        HttpErrors.Service_Unavailable_Title,
                        HttpErrors.Temporarily_Unavailable_Detail
                    );
                    break;
                case DbUpdateConcurrencyException:
                    await WriteProblemDetailsAsync(
                        context,
                        ProblemDetailsBuilder.BuildProblemDetails(null, traceId),
                        409,
                        HttpErrors.Conflict_Title,
                        HttpErrors.Conflict_Detail
                    );
                    break;
                case DbUpdateException:
                    await WriteProblemDetailsAsync(
                        context,
                        ProblemDetailsBuilder.BuildProblemDetails(null, traceId),
                        500,
                        HttpErrors.Internal_Server_Error_Title,
                        HttpErrors.Database_Detail
                    );
                    break;
                default:
                    await WriteProblemDetailsAsync(
                        context,
                        ProblemDetailsBuilder.BuildProblemDetails(null, traceId),
                        500,
                        HttpErrors.Internal_Server_Error_Title,
                        HttpErrors.Unexpected_Error_Detail
                    );
                    break;
            }
        }
    }

    private void LogException(HttpContext context, Exception ex)
    {
        _logger.LogError(
            ex,
            LogMessageConstant.UnhandledException,
            context.Request.Path,
            context.Request.Method,
            context.Response.StatusCode,
            ex.GetType().Name,
            ex.Message
        );
    }

    private static async Task WriteProblemDetailsAsync(
        HttpContext context,
        ProblemDetails problemDetails,
        int status,
        string title,
        string detail
    )
    {
        problemDetails.Status = status;
        problemDetails.Title = title;
        problemDetails.Detail = detail;
        problemDetails.Instance = context.Request.Path;
        context.Response.StatusCode = status;
        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}
