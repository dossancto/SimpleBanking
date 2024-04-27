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
    public async Task<UniqueContatOutput> Execute(UniqueContactInput input)
    {
        var person = await _personRepository.SearchByTerm(new()
        {
            Term = input.SeachTerm
        });

        if (person is not null)
        {
            return new()
            {
                ConflictField = input.SeachTerm,
                ConflictId = person.Id,
                ConflitUser = ConlitedEnum.Person
            };
        }

        return UniqueContatOutput.Unique();
    }

    /// <summary>
    /// Execute the method
    /// </summary>
    /// <returns>Boolean indicating if the contact infos is unique</returns>
    public async Task<UniqueContatOutput> Execute(UniqueContactInfosInput input)
    {
        foreach (var f in input.Fields())
        {
            if (f is null)
                continue;

            var res = await Execute(new UniqueContactInput()
            {
                SeachTerm = f
            });

            if (res.IsUnique)
            {
                continue;
            }

            return new()
            {
                ConflictField = f,
                ConflictId = res.ConflictId,
                ConflitUser = res.ConflitUser
            };
        }

        return UniqueContatOutput.Unique();
    }
}
