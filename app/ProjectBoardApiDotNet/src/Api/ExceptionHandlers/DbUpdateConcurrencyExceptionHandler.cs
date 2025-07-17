namespace Api.ExceptionHandlers;

internal sealed class DbUpdateConcurrencyExceptionHandler
    : BaseExceptionHandler<DbUpdateConcurrencyException>
{
    public DbUpdateConcurrencyExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<DbUpdateConcurrencyExceptionHandler> logger
    )
        : base(problemDetailsService, logger) { }

    protected override int StatusCode => StatusCodes.Status409Conflict;
    protected override string Title => HttpErrors.Conflict_Title;
    protected override string Detail => HttpErrors.Conflict_Detail;
}