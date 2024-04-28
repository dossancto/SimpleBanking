namespace SimpleBanking.Application.Features.Persons.UseCases.AddAmmount;

public class AddAmmountInput
{
    public required int Balance { get; set; }
    public required string Person { get; set; }
}
