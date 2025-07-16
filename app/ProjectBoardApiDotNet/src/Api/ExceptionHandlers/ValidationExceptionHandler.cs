namespace Api.ExceptionHandlers;

internal sealed class ValidationExceptionHandler : BaseExceptionHandler<ValidationException>
{
    public ValidationExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<ValidationExceptionHandler> logger
    )
        : base(problemDetailsService, logger) { }

    protected override int StatusCode => StatusCodes.Status400BadRequest;
    protected override string Title => HttpErrors.Validation_Title;
    protected override string Detail => HttpErrors.Validation_Detail;
    protected override LogLevel LogLevel => LogLevel.Warning;

    public override async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ValidationException validationException)
            return false;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        var errors = validationException
            .Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key, // Use original casing for property names
                g => g.Select(e => e.ErrorMessage).ToArray()
            );

        var validationProblemDetails = new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = ProblemDetailsUriHelper.GetProblemTypeUri(StatusCodes.Status400BadRequest),
            Title = HttpErrors.Validation_Title,
            Detail = HttpErrors.Validation_Detail,
        };
        validationProblemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

        return await ProblemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = validationProblemDetails,
            }
        );
    }
}
