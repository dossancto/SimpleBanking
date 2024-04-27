using Testcontainers.PostgreSql;

namespace SimpleBanking.Tests.Integration.Setup;

public sealed class PostgresContainerSingleton
{
    private static readonly Lazy<PostgreSqlContainer> _instance =
         new(GetPostgreSqlContainer);

    public static PostgreSqlContainer Instance => _instance.Value;

    private static readonly Lazy<bool> _migrationsRunned = new(false);

    public static bool MigrationsRunned => _migrationsRunned.Value;

    private static PostgreSqlContainer GetPostgreSqlContainer()
    {
        Console.WriteLine("Using a new Postgres container");

        return new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase(DatabaseSettings.DatabaseName)
                .WithUsername(DatabaseSettings.Username)
                .WithPassword(DatabaseSettings.Password)
                .Build();
    }
}


