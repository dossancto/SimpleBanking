using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Domain.Exceptions;
using SimpleBanking.Domain.Features.Persons.Entities;

namespace SimpleBanking.Application.Features.Persons.UseCases.AddAmmount;

public class AddAmmountUseCase(
    IPersonRepository _personRepository
    ) : IUseCase
{
    public async Task<SafePerson> Execute(AddAmmountInput input)
    {
        var p = await _personRepository.SearchByTerm(new()
        {
            Term = input.Person
        })
        ?? throw new NotFoundException($"Person with term {input.Person} not found");

        await _personRepository.MoveBalance(p.Id, input.Balance);

        return await _personRepository.SelectById(new()
        {
            Id = p.Id
        })!.SafeData();
    }
}
