using Microsoft.EntityFrameworkCore;
using SimpleBanking.Infra.Database.EF.Contexts;
using SimpleBanking.Infra.Utils.Anvs;

namespace SimpleBanking.DependencyInjection.Providers;

internal static class DatabaseInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    { 
        var connectionString = Anv.Database.POSTGRES_CONNECTION_STRING.NotNull();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}

