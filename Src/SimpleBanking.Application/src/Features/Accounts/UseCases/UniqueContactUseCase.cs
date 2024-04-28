using SimpleBanking.Application.Features.Merchants.Data;
using SimpleBanking.Application.Features.Persons.Data;

namespace SimpleBanking.Application.Features.Accounts.UseCases;

/// <summary>
/// Represents a usecase for asserting that unique informaitons are really unique
/// </summary>
public sealed class UniqueContactUseCase(
    IPersonRepository _personRepository,
    IMerchantRepository _merchantRepository
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
                Data = person,
                UserType = ConlitedEnum.Person
            };
        }

        var merchant = await _merchantRepository.SearchByTerm(new()
        {
            Term = input.SeachTerm
        });

        if (merchant is not null)
        {
            return new()
            {
                ConflictField = input.SeachTerm,
                ConflictId = merchant.Id,
                Data = merchant,
                UserType = ConlitedEnum.Merchant
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

            res.ConflictField = f;

            return res;
        }

        return UniqueContatOutput.Unique();
    }
}
