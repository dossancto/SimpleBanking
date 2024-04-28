using Aether.Leagues.Adapters.UnitOfWorks;

namespace SimpleBanking.Tests.Unit.Mocks;

public class UnitOfWorkMock : IUnitOfWork
{
    public virtual Task Apply()
    {
        return Task.CompletedTask;
    }

    public virtual Task Begin()
    {
        return Task.CompletedTask;
    }

    public void Dispose() { }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public virtual Task Finish()
    {
        return Task.CompletedTask;
    }

    public virtual Task Rollback()
    {
        return Task.CompletedTask;
    }
}
