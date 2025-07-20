using FluentResults;

namespace Common.Domain.Errors;

public class CustomError : Error
{
    public string Title { get; }

    public ErrorType ErrorType { get; }

    public CustomError(string title, string detail, ErrorType errorType)
        : base(detail)
    {
        Title = title;
        ErrorType = errorType;
    }
}
