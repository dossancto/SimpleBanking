using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleBanking.Infra.Database.EF.Contexts;
using SimpleBanking.Infra.Utils.Anvs;
using Testcontainers.PostgreSql;

namespace SimpleBanking.Tests.Integration.Setup;

public static class TestInjection
{
    public static IServiceCollection AddTestEnvironment(this IServiceCollection services, PostgreSqlContainer postgreSqlContainer)
    {
        Console.WriteLine("Start db injection");
        services.AddDatabasesInjection(postgreSqlContainer);
        Console.WriteLine("Finish db injection");
        // services.ReplaceHelpers();

        return services;
    }

    // public static IServiceCollection ReplaceHelpers(this IServiceCollection services)
    //   => services
    //        .ConfigureHelpers();

    public static void AddEnvironmentVariables(string connectionString)
    {
        var addEnv = (string envName, string val) => Environment.SetEnvironmentVariable(envName, val);

        addEnv("POSTGRES_CONNECTION_STRING", connectionString);

        addEnv("JWT_SECRET_KEY", "HGrPBCW8DPbxA-DT0wm3jhaJZQektl2mxmPb");
        addEnv("JWT_ISSUER", "HGrPBCW8DPbxA-DT0wm3jhaJZQektl2mxmPb");
        addEnv("JWT_AUDIENCE", "HGrPBCW8DPbxA-DT0wm3jhaJZQektl2mxmPb");

        addEnv("RABBITMQ_HOST", "localhost");
        addEnv("RABBITMQ_PORT", "5672");
        addEnv("RABBITMQ_USER", "guest");
        addEnv("RABBITMQ_PASSWORD", "guest");
    }

    private static IServiceCollection AddDatabasesInjection(this IServiceCollection services, PostgreSqlContainer postgreSqlContainer)
    {
        var connectionstring = postgreSqlContainer.GetConnectionString();

        var useLogs = Boolean.Parse(Anv.Test.USE_EF_LOG.OrDefault("true"));

        Console.WriteLine("Start running migrations");
        if (!PostgresContainerSingleton.MigrationsRunned)
        {
            // services.RunPrismaMigrations(postgreSqlContainer);
        }
        Console.WriteLine("Finish running migrations");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionstring);
            if (!useLogs)
            {
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFilter((category, level) => false)));
                options.EnableSensitiveDataLogging(false); // Disable sensitive data logging
                options.EnableDetailedErrors(false); // Disable detailed error messages
                options.LogTo(Console.WriteLine, LogLevel.None); // Set logging level to None
            }
            else
            {
                // options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFilter((category, level) => false)));
                options.EnableSensitiveDataLogging(true); // Disable sensitive data logging
                options.EnableDetailedErrors(true); // Disable detailed error messages
            }
        });

        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        db.Database.EnsureCreated();

        services.AddLogging(builder =>
           {
               builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
           });

        return services;
    }

    // private static IServiceCollection RunPrismaMigrations(this IServiceCollection services, PostgreSqlContainer postgreSqlContainer)
    // {
    //     var connectionString = postgreSqlContainer.GetConnectionString();
    //     var workingDir = $"{System.IO.Directory.GetCurrentDirectory()}/../../../../../migrations";

    //     var startInfo = new ProcessStartInfo
    //     {
    //         FileName = "npx",
    //         Arguments = "prisma migrate deploy",
    //         RedirectStandardOutput = true,
    //         UseShellExecute = false,
    //         CreateNoWindow = false,
    //         WorkingDirectory = workingDir
    //     };

    //     startInfo.EnvironmentVariables["DATABASE_URL"] = $"postgresql://{DatabaseSettings.Username}:{DatabaseSettings.Password}@localhost:{postgreSqlContainer.GetMappedPublicPort(5432)}/{DatabaseSettings.DatabaseName}?schema=public";

    //     using var process = Process.Start(startInfo);

    //     string output = process!.StandardOutput.ReadToEnd();

    //     process.WaitForExit();

    //     Console.WriteLine(output);

    //     return services;
    // }
}

