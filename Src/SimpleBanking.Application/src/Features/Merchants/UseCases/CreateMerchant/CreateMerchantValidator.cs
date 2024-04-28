using FluentValidation;
using SimpleBanking.Application.Validations.CustomValidations;

namespace SimpleBanking.Application.Features.Merchants.UseCases.CreateMerchant;

public class CreateMerchantValidator : AbstractValidator<CreateMerchantInput>
{
    public CreateMerchantValidator()
    {
        RuleFor(x => x.Password).StrongPassword();

        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);

        RuleFor(x => x.FullName).NotEmpty().MinimumLength(5).MaximumLength(128);
    }
}
