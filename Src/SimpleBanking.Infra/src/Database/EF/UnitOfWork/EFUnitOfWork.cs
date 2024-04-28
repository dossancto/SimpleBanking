using Aether.Leagues.Adapters.UnitOfWorks;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleBanking.Infra.Database.EF.Contexts;

namespace SimpleBanking.Infra.Database.EF.UnitOfWork;

public class EFUnitOfWork(ApplicationDbContext _context) : IUnitOfWork
{
    private IDbContextTransaction? Transaction { get; set; }

    public void Dispose()
    {
        if (Transaction is not null)
        {
            Transaction?.Dispose();
            Transaction = null;
        }
    }

    public async Task Begin()
    {
        if (_context.Database.CurrentTransaction is null)
        {
            Transaction = await _context.Database.BeginTransactionAsync();
        }
    }

    public Task Finish()
    {
        Dispose();
        return Task.CompletedTask;
    }

    public async Task Rollback()
    {
        if (_context.Database.CurrentTransaction is null || Transaction is null) return;

        await Transaction.RollbackAsync();
    }

    public async Task Apply()
    {
        if (_context.Database.CurrentTransaction is null || Transaction is null) return;

        await Transaction.CommitAsync();
    }

}

