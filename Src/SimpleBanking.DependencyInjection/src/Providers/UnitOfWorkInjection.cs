using Aether.Leagues.Adapters.UnitOfWorks;
using SimpleBanking.Infra.Database.EF.UnitOfWork;

namespace SimpleBanking.DependencyInjection.Providers;

internal static class UnitOfWorkInjection
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
      => services
          .AddTransient<IUnitOfWork, EFUnitOfWork>()
      ;
}

