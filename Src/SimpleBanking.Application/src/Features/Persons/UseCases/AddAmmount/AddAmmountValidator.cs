using FluentValidation;

namespace SimpleBanking.Application.Features.Persons.UseCases.AddAmmount;

public class AddAmmountValidator : AbstractValidator<AddAmmountInput>
{
    public AddAmmountValidator()
    {
        RuleFor(x => x.Person).NotEmpty();

        RuleFor(x => x.Balance).GreaterThan(0);
    }
}
