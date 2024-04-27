using FluentValidation;
using SimpleBanking.Application.Validations.CustomValidations;

namespace SimpleBanking.Application.Features.Persons.UseCases.CreatePerson;

public class CreatePersonValidator : AbstractValidator<CreatePersonInput>
{
    public CreatePersonValidator()
    {
        RuleFor(x => x.Password).StrongPassword();

        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);

        RuleFor(x => x.FullName).NotEmpty().MinimumLength(5).MaximumLength(256);

        RuleFor(x => x.CPF).ValidCPF();
    }
}

