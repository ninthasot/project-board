using Api.Constants;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Errors;

namespace Api.Helpers;

public static class ProblemDetailsBuilder
{
    public static ProblemDetails BuildProblemDetails(IError? error, string? traceId = null)
    {
        ProblemDetails details = error switch
        {
            CustomError ce => new ProblemDetails
            {
                Status = ce.StatusCode,
                Title = ce.Code,
                Detail = ce.Message,
            },
            IError ie => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = HttpConstant.ProblemTitleInternalServerError,
                Detail = ie.Message,
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = HttpConstant.ProblemTitleInternalServerError,
                Detail = HttpConstant.ProblemDetailUnexpectedError,
            },
        };

        if (!string.IsNullOrEmpty(traceId))
            details.Extensions["traceId"] = traceId;

        return details;
    }

    public static ValidationProblemDetails BuildValidationProblemDetails(
        IEnumerable<ValidationError> validationErrors,
        string? traceId = null
    )
    {
        var problemDetails = new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = HttpConstant.ProblemTitleValidation,
            Detail = HttpConstant.ProblemDetailValidationError,
        };
        if (!string.IsNullOrWhiteSpace(traceId))
            problemDetails.Extensions["traceId"] = traceId;
        problemDetails.Errors.Add(
            "ValidationErrors",
            validationErrors.Select(e => e.Message).ToArray()
        );
        return problemDetails;
    }
}
