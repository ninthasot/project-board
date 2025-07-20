using FluentResults;

namespace Common.Domain.Errors;

public class ValidationError : CustomError
{
    public IReadOnlyList<CustomError> Errors { get; }

    public ValidationError(IEnumerable<CustomError> errors)
        : base(
            Common_Domain_Resource.Validation_Error_Title,
            Common_Domain_Resource.Validation_Error_Detail,
            ErrorType.Validation
        )
    {
        Errors = errors?.ToList() ?? new List<CustomError>();
    }

    public static ValidationError FromResults(IEnumerable<Result> results) =>
        new(results.Where(r => r.IsFailed).SelectMany(r => r.Errors.OfType<CustomError>()));
}
