namespace Api.ExceptionHandlers;

internal sealed class ServiceUnavailableExceptionHandler : BaseExceptionHandler<Exception>
{
    public ServiceUnavailableExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<ServiceUnavailableExceptionHandler> logger
    )
        : base(problemDetailsService, logger) { }

    protected override int StatusCode => StatusCodes.Status503ServiceUnavailable;
    protected override string Title => HttpErrors.Service_Unavailable_Title;
    protected override string Detail => HttpErrors.Temporarily_Unavailable_Detail;
}
