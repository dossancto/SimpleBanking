namespace SimpleBanking.Tests.Integration.Utils;

public abstract class IntegrationTest(IntegrationTestWebApplicationFactory web) : IClassFixture<IntegrationTestWebApplicationFactory>, IAsyncLifetime
{
    public Task InitializeAsync() => web._postgreSqlContainer.StartAsync();

    Task IAsyncLifetime.DisposeAsync()
      => Task.CompletedTask;
}

