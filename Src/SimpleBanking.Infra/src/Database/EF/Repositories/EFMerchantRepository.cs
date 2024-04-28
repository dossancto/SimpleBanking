using Microsoft.EntityFrameworkCore;
using SimpleBanking.Domain.Features.Balances.Exceptions;
using SimpleBanking.Application.Features.Merchants.Data;
using SimpleBanking.Domain.Features.Merchants.Entities;
using SimpleBanking.Infra.Database.EF.Contexts;
using SimpleBanking.Infra.Providers.IdGenerator;
using SimpleBanking.Application.Features.Merchants.UseCases.SelectMerchant;

namespace SimpleBanking.Infra.Database.EF.Repositories;

public class EFMerchantRepository(
    ApplicationDbContext _context,
    IIdGenerator idGenerator
   ) : IMerchantRepository
{
    public async Task<string> Insert(Merchant entity)
    {
        entity.Id = idGenerator.Generate();

        var createdUser = _context.Merchants.Add(entity);

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task MoveBalance(string TargetId, int ammount)
    {
        var entity = await _context.Merchants
                  .Where(x => x.Id == TargetId)
                  .ExecuteUpdateAsync(s =>
                      s.SetProperty(
                        p => p.Debit,
                        p => p.Debit + ammount
                        ));

        if (entity != 1)
        {
            throw new TransferException("Fail while updating balance");
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Merchant?> SearchByTerm(SelectMerchantByTermInput input)
    => await _context.Merchants
                  .Where(x => x.Search.Contains(input.Term))
                  .FirstOrDefaultAsync()
                  ;


    // public async Task<Merchant?> SelectById(SelectMerchantByIdInput input)
    // => await _context.Merchants
    //               .FindAsync(input.Id)
    //               ;
}


