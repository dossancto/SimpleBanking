using SimpleBanking.Application.Features.Balances.Data;
using SimpleBanking.Application.Features.Merchants.UseCases.SelectMerchant;
using SimpleBanking.Domain.Features.Merchants.Entities;

namespace SimpleBanking.Application.Features.Merchants.Data;

public interface IMerchantRepository : IBalanceMoveRepository
{
    Task<string> Insert(Merchant entity);

    /// <summary>
    /// Searches a term 
    /// </summary>
    /// <returns>The seached merchant or null if not found</returns>
    public Task<Merchant?> SearchByTerm(SelectMerchantByTermInput input);
}
