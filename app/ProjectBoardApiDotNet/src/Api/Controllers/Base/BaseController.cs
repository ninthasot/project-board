namespace Api.Controllers.Base;

public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator { get; }

    protected BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected ActionResult ToActionResult<T>(
        Result<T>? result,
        Func<T, ActionResult> successFactory
    )
    {
        if (result is null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        if (result.IsSuccess)
        {
            ArgumentNullException.ThrowIfNull(successFactory);
            return successFactory(result.Value);
        }

        var traceId = HttpContext?.TraceIdentifier;

        // Aggregate validation errors
        var validationErrors = result.Errors.OfType<ValidationError>().ToList();
        if (validationErrors.Count > 0)
            return BadRequest(
                ProblemDetailsBuilder.BuildValidationProblemDetails(validationErrors, traceId)
            );

        var error = result.Errors.Count > 0 ? result.Errors[0] : null;
        if (error is null)
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                ProblemDetailsBuilder.BuildProblemDetails(null, traceId)
            );

        return error switch
        {
            NotFoundError e => string.IsNullOrEmpty(e.Message)
                ? NotFound()
                : NotFound(ProblemDetailsBuilder.BuildProblemDetails(e, traceId)),
            ConflictError e => Conflict(ProblemDetailsBuilder.BuildProblemDetails(e, traceId)),
            BadRequestError e => BadRequest(ProblemDetailsBuilder.BuildProblemDetails(e, traceId)),
            CustomError e => StatusCode(
                StatusCodes.Status500InternalServerError,
                ProblemDetailsBuilder.BuildProblemDetails(e, traceId)
            ),
            _ => StatusCode(
                StatusCodes.Status500InternalServerError,
                ProblemDetailsBuilder.BuildProblemDetails(error, traceId)
            ),
        };
    }

    protected ActionResult ToOkActionResult<T>(Result<T>? result) =>
        ToActionResult(result, value => Ok(value));

    protected ActionResult ToNoContentActionResult<T>(Result<T>? result) =>
        ToActionResult(result, _ => NoContent());

    protected ActionResult ToCreatedActionResult<T>(
        Result<T>? result,
        string actionName,
        object? routeValues = null
    ) => ToActionResult(result, value => CreatedAtAction(actionName, routeValues, value));
}
