using FluentValidation;
using SimpleBanking.Application.Validations.CustomValidations;

namespace SimpleBanking.Application.Features.Persons.UseCases.CreatePerson;

public class CreatePersonValidator : AbstractValidator<CreatePersonInput>
{
    public CreatePersonValidator()
    {
        RuleFor(x => x.Password).StrongPassword();

        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);

        RuleFor(x => x.FullName).NotEmpty().MinimumLength(5).MaximumLength(128);

        RuleFor(x => x.CPF).ValidCPF();
    }
}

