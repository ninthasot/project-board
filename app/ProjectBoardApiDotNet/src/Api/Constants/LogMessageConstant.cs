namespace Api.Constants;

public static class LogMessageConstant
{
    public const string UnhandledException =
        "Unhandled exception occurred while processing request. Path: {Path}, Method: {Method}, StatusCode: {StatusCode}, ExceptionType: {ExceptionType}, Message: {Message}";
}
