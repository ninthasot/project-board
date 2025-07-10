using FluentResults;

namespace Domain.Errors;

public class CustomError : Error
{
    public string Code { get; }
    public int StatusCode { get; }
    public string? StackTrace { get; }

    public CustomError(int statusCode, string code, string message, string? stackTrace = null)
        : base(message)
    {
        Code = code;
        StatusCode = statusCode;
        StackTrace = stackTrace;
    }
}