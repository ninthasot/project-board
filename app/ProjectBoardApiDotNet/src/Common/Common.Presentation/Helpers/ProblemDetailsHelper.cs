using Common.Domain.Errors;
using Common.Presentation.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Presentation;

public static class ProblemDetailsHelper
{
    public static ProblemDetails ProblemDetailsBuilder(CustomError error, string? traceId)
    {
        ArgumentNullException.ThrowIfNull(error);

        if (error is ValidationError validationError)
            return ValidationProblemDetailsBuilder(validationError, traceId);

        var problemDetails = new ProblemDetails();

        problemDetails.Title = error.Title;
        problemDetails.Detail = error.Message;
        problemDetails.Status = GetStatus(error);
        if (problemDetails.Status is not null)
            problemDetails.Type = ProblemDetailsUriHelper.GetProblemTypeUri(
                (int)problemDetails.Status
            );

        if (!string.IsNullOrEmpty(traceId))
            problemDetails.Extensions["traceId"] = traceId;

        return problemDetails;
    }

    private static int? GetStatus(CustomError error)
    {
        return error.ErrorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Problem => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };
    }

    private static ValidationProblemDetails ValidationProblemDetailsBuilder(
        ValidationError error,
        string? traceId
    )
    {
        var errors = error
            .Errors.GroupBy(e => e.Title)
            .ToDictionary(g => g.Key, g => g.Select(e => e.Message).ToArray());

        var validationProblemDetails = new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = ProblemDetailsUriHelper.GetProblemTypeUri(StatusCodes.Status400BadRequest),
            Title = error.Title,
            Detail = error.Message,
        };

        if (!string.IsNullOrEmpty(traceId))
            validationProblemDetails.Extensions["traceId"] = traceId;

        return validationProblemDetails;
    }
}
