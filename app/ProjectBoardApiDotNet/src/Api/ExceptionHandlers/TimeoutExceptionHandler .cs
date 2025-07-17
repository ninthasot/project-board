namespace Api.ExceptionHandlers;

internal sealed class TimeoutExceptionHandler : BaseExceptionHandler<TimeoutException>
{
    public TimeoutExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<TimeoutExceptionHandler> logger
    )
        : base(problemDetailsService, logger) { }

    protected override int StatusCode => StatusCodes.Status503ServiceUnavailable;
    protected override string Title => HttpErrors.Service_Unavailable_Title;
    protected override string Detail => HttpErrors.Temporarily_Unavailable_Detail;
}