using SimpleBanking.Domain.Exceptions;

using FluentValidation;

namespace SimpleBanking.Application.Validations;

/// <summary>
/// Extends FluentValidation to apply Check
/// </summary>
public static class ValidationCheckerExtension
{
    /// <summary>
    /// Apply FluentValidation checker, if invalid throws Exception
    /// </summary>
    public static void Check<T>(this IValidator<T> validator, T value, string message)
    {
        var result = validator.Validate(value);
        HandleResult(result, message);
    }

    /// <summary>
    /// Apply async FluentValidation checker, if invalid throws ExceptioN
    /// </summary>
    public static async Task CheckAsync<T>(this IValidator<T> validator, T value, string message)
    {
        var result = await validator.ValidateAsync(value);

        HandleResult(result, message);
    }

    private static void HandleResult(FluentValidation.Results.ValidationResult result, string message)
    {
        if (result.IsValid)
        {
            return;
        }

        var errors = result
          .Errors
          .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
          .ToList();

        throw new ValidationFailException(message, errors);
    }
}
