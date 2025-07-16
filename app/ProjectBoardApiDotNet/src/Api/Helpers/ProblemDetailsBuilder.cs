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
                Title = HttpErrors.Internal_Server_Error_Title,
                Detail = ie.Message,
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = HttpErrors.Internal_Server_Error_Title,
                Detail = HttpErrors.Unexpected_Error_Detail,
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
            Title = HttpErrors.Validation_Title,
            Detail = HttpErrors.Validation_Detail,
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
