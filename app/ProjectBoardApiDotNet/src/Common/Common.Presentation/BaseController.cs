using Common.Domain.Errors;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Presentation;

public class BaseController : ControllerBase
{
    protected ISender Sender { get; }

    public BaseController(ISender sender)
    {
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    protected ActionResult<T> ToOkActionResult<T>(Result<T> result)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (result.IsSuccess)
            return Ok(result.Value);

        return ToErrorActionResult<T>(result);
    }

    protected ActionResult<T> ToCreatedAtActionResult<T>(Result<T> result, string? actionName)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (result.IsSuccess)
            return CreatedAtAction(actionName, new { id = result.Value }, result.Value);

        return ToErrorActionResult<T>(result);
    }

    protected ActionResult<T> ToErrorActionResult<T>(Result<T> result)
    {
        if (result is null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        var error = result.Errors.Count > 0 ? result.Errors[0] : null;

        var customError = error as CustomError;

        if (customError is null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        var problemDetails = ProblemDetailsHelper.ProblemDetailsBuilder(
            customError,
            HttpContext?.TraceIdentifier
        );

        return customError.ErrorType switch
        {
            ErrorType.NotFound => NotFound(problemDetails),
            ErrorType.Conflict => Conflict(problemDetails),
            ErrorType.Validation => BadRequest(problemDetails),
            ErrorType.Problem => BadRequest(problemDetails),
            _ => BadRequest(problemDetails),
        };
    }
}
