using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using SimpleBanking.Adapters.MessageBrokers;
using SimpleBanking.Infra.Database.EF.Contexts;
using SimpleBanking.Tests.Integration.Setup;
using Testcontainers.PostgreSql;

namespace SimpleBanking.Tests.Integration;

public static class DatabaseSettings
{
    public const string DatabaseName = "aether-league-integration-test";
    public const string Username = "postgres";
    public const string Password = "postgres";
}

public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>
{
    public readonly PostgreSqlContainer _postgreSqlContainer = PostgresContainerSingleton.Instance;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        TestInjection.AddEnvironmentVariables(_postgreSqlContainer.GetConnectionString());

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.RemoveAll(typeof(IMessageBrokerProvider));
            // services.RemoveAll(typeof(IMessageBrokerProvider));

            Console.WriteLine("Remove old dependencies");
            services.AddTestEnvironment(_postgreSqlContainer);

            services.AddScoped<IMessageBrokerProvider>(_ => Substitute.For<IMessageBrokerProvider>());

            // var messageBrokerMock = new Mock<IMessageBrokerProvider>();

            // services.AddScoped(_ => messageBrokerMock.Object);
        });
    }
}
