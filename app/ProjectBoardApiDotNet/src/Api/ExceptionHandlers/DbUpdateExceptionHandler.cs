namespace Api.ExceptionHandlers;

internal sealed class DbUpdateExceptionHandler : BaseExceptionHandler<DbUpdateException>
{
    public DbUpdateExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<DbUpdateExceptionHandler> logger
    )
        : base(problemDetailsService, logger) { }

    protected override int StatusCode => StatusCodes.Status500InternalServerError;
    protected override string Title => HttpErrors.Internal_Server_Error_Title;
    protected override string Detail => HttpErrors.Database_Detail;
}