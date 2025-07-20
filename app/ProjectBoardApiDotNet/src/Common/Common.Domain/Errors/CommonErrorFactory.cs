namespace Common.Domain.Errors;

public static class CommonErrorFactory
{
    public static readonly CustomError NullValue = new CustomError(
        Common_Domain_Resource.Null_Value_Error_Title,
        Common_Domain_Resource.Null_Value_Error_Detail,
        ErrorType.Failure
    );

    public static CustomError Failure(string code, string detail) =>
        new(code, detail, ErrorType.Failure);

    public static CustomError NotFound(string code, string detail) =>
        new(code, detail, ErrorType.NotFound);

    public static CustomError Problem(string code, string detail) =>
        new(code, detail, ErrorType.Problem);

    public static CustomError Conflict(string code, string detail) =>
        new(code, detail, ErrorType.Conflict);
}
