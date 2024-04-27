using SimpleBanking.Infra.Providers.IdGenerator;

namespace SimpleBanking.DependencyInjection.Providers;

internal static class IdGeneratorInjection
{
    public static IServiceCollection AddIdGenerator(this IServiceCollection services)
      => services
          .AddTransient<IIdGenerator, NanoIdGenerator>()
      ;

}

