using Microsoft.EntityFrameworkCore;
using SimpleBanking.Domain.Features.Balances.Exceptions;
using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Application.Features.Persons.UseCases.SelectPerson;
using SimpleBanking.Domain.Features.Persons.Entities;
using SimpleBanking.Infra.Database.EF.Contexts;
using SimpleBanking.Infra.Providers.IdGenerator;

namespace SimpleBanking.Infra.Database.EF.Repositories;

public class EFPersonRepository(
    ApplicationDbContext _context,
    IIdGenerator idGenerator
   ) : IPersonRepository
{
    public async Task<string> Insert(Person entity)
    {
        entity.Id = idGenerator.Generate();

        var createdUser = _context.Persons.Add(entity);

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task MoveBalance(string TargetId, int ammount)
    {
        var entity = await _context.Persons
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

    public Task<Person?> SearchByTerm(SearchPersonByTermInput input)
    {
        throw new NotImplementedException();
    }

    public Task<Person?> SelectById(SelectPersonByIdInput input)
    {
        throw new NotImplementedException();
    }
}

