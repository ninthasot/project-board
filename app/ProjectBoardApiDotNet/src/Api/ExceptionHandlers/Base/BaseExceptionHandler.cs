namespace Api.ExceptionHandlers;

internal abstract class BaseExceptionHandler<TException> : IExceptionHandler
    where TException : Exception
{
    protected readonly IProblemDetailsService ProblemDetailsService;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;

    protected BaseExceptionHandler(
        IProblemDetailsService problemDetailsService,
        Microsoft.Extensions.Logging.ILogger logger
    )
    {
        ProblemDetailsService = problemDetailsService;
        _logger = logger;
    }

    protected abstract int StatusCode { get; }
    protected abstract string Title { get; }
    protected abstract string Detail { get; }

    protected virtual LogLevel LogLevel => LogLevel.Error;

    public virtual async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not TException)
            return false;

        _logger.Log(
            LogLevel,
            exception,
            LogMessageConstant.UnhandledException,
            httpContext.Request.Path,
            httpContext.Request.Method,
            StatusCode,
            exception.GetType().Name,
            exception.Message,
            exception.StackTrace,
            httpContext.TraceIdentifier,
            httpContext.User?.Identity?.Name ?? "Anonymous",
            exception.InnerException?.ToString(),
            exception.Source
        );

        httpContext.Response.StatusCode = StatusCode;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCode,
            Type = ProblemDetailsUriHelper.GetProblemTypeUri(StatusCode),
            Title = Title,
            Detail = Detail,
            Extensions = { ["traceId"] = httpContext.TraceIdentifier },
        };

        return await ProblemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = problemDetails,
            }
        );
    }
}
