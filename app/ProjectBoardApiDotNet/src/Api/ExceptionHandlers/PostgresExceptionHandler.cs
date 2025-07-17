namespace Api.ExceptionHandlers;

internal class PostgresExceptionHandler : BaseExceptionHandler<PostgresException>
{
    public PostgresExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<PostgresExceptionHandler> logger
    )
        : base(problemDetailsService, logger) { }

    protected override int StatusCode => StatusCodes.Status503ServiceUnavailable;
    protected override string Title => HttpErrors.Service_Unavailable_Title;
    protected override string Detail => HttpErrors.Temporarily_Unavailable_Detail;
}