using System.Reflection;
using Common.Application.Messaging;
using Common.Domain.Errors;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Common.Application.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );
        var failures = validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors).ToArray();

        if (failures.Length == 0)
        {
            return await next();
        }

        var validationError = CreateValidationError(failures);

        // Handle Result<T>
        if (
            typeof(TResponse).IsGenericType
            && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>)
        )
        {
            var resultType = typeof(TResponse).GetGenericArguments()[0];
            var failMethod = typeof(Result)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(m =>
                    m.Name == nameof(Result.Fail)
                    && m.IsGenericMethodDefinition
                    && m.GetParameters().Length == 1
                    && typeof(IError).IsAssignableFrom(m.GetParameters()[0].ParameterType)
                );

            if (failMethod is not null)
            {
                var genericFail = failMethod.MakeGenericMethod(resultType);
                return (TResponse)genericFail.Invoke(null, new object[] { validationError })!;
            }
        }
        // Handle Result
        else if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.Fail(validationError);
        }

        // Fallback: throw for other response types
        throw new ValidationException(failures);
    }

    private static ValidationError CreateValidationError(ValidationFailure[] failures) =>
        new(
            failures
                .Select(f => new CustomError(f.PropertyName, f.ErrorMessage, ErrorType.Validation))
                .ToArray()
        );
}
