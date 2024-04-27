using FluentValidation;

namespace SimpleBanking.Application.Validations.CustomValidations;

/// <summary>
/// Apply Methods for Password validations
/// </summary>
public static class PasswordValidation
{
    /// <summary>
    /// Apply Rules for Strong Password
    /// </summary>
    public static IRuleBuilder<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
      => ruleBuilder
         .NotEmpty().WithMessage("Password can not be empty.")
         .MinimumLength(8).WithMessage("Password must contain at least 8 characters.")
         .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
         .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
         .Matches(@"\d").WithMessage("Password must contain at least one digit.")
         .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
}
