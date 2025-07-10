using FluentResults;

namespace Domain.Errors.Factories;

public static class SharedErrorFactory
{
    public static NotFoundError NotFound { get; } = new NotFoundError();
    public static ArgumentNullError ArgumentNull { get; } = new ArgumentNullError();

    public static Error ExceptionalError(Exception e) => new ExceptionalError(e);
}