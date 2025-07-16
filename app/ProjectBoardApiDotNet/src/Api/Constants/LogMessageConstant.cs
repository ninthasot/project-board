namespace Api.Constants;

public static class LogMessageConstant
{
    public const string UnhandledException =
        "Unhandled exception occurred. Path: {Path}, Method: {Method}, StatusCode: {StatusCode}, ExceptionType: {ExceptionType}, Message: {Message}, StackTrace: {StackTrace}, TraceId: {TraceId}, User: {User}, InnerException: {InnerException}, Source: {Source}";
}
