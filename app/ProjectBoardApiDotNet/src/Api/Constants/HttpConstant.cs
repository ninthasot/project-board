namespace Api.Constants;

public static class HttpConstant
{
    public const string ProblemTitleServiceUnavailable = "Service Unavailable";
    public const string ProblemTitleConflict = "Conflict";
    public const string ProblemTitleInternalServerError = "Internal Server Error";
    public const string ProblemTitleBadRequest = "Bad Request";
    public const string ProblemTitleValidation = "Validation Failed";
    public const string ProblemDetailTemporarilyUnavailable =
        "The service is temporarily unavailable. Please try again later";
    public const string ProblemDetailConflict =
        "A conflict occurred while processing your request. Please try again.";
    public const string ProblemDetailUnexpectedError =
        "An unexpected error occurred. Please try again later.";
    public const string ProblemDetailDatabaseError =
        "A database error occurred. Please try again later.";
    public const string ProblemDetailValidationError = "One or more validation errors occurred.";
}
