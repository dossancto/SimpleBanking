using SimpleBanking.Application.Features.Persons.Data;

namespace SimpleBanking.Application.Features.Accounts.UseCases;

/// <summary>
/// Represents a usecase for asserting that unique informaitons are really unique
/// </summary>
public sealed class UniqueContactUseCase(
    IPersonRepository _personRepository
) : IUseCase
{
    /// <summary>
    /// Execute the method
    /// </summary>
    /// <returns>Boolean indicating if the contact infos is unique</returns>
    public async Task<bool> Execute(UniqueContactInput input)
    {
        var person = await _personRepository.SearchByTerm(new()
        {
            Term = input.SeachTerm
        });

        if (person is not null)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Execute the method
    /// </summary>
    /// <returns>Boolean indicating if the contact infos is unique</returns>
    public async Task<string?> Execute(UniqueContactInfosInput input)
    {
        foreach (var f in input.Fields())
        {
            if (f is null)
                continue;

            var res = await Execute(new UniqueContactInput()
            {
                SeachTerm = f
            });

            if (res)
            {
                continue;
            }

            return f;
        }

        return null;
    }
}
