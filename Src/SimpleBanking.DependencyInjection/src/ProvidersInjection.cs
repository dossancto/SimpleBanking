using SimpleBanking.DependencyInjection.Others;
using SimpleBanking.DependencyInjection.Providers;

namespace SimpleBanking.DependencyInjection;

public static class ProvidersInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
      => services
            .AddMediator()
            .AddFluentValidationConfiguration()
            .AddIdGenerator()
            .AddHasherProvider()
      ;
}
