namespace Api.ExceptionHandlers;

internal sealed class GlobalExceptionHandler : BaseExceptionHandler<Exception>
{
    public GlobalExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger
    )
        : base(problemDetailsService, logger) { }

    protected override int StatusCode => StatusCodes.Status500InternalServerError;
    protected override string Title => HttpErrors.Internal_Server_Error_Title;
    protected override string Detail => HttpErrors.Internal_Server_Error_Detail;
}
