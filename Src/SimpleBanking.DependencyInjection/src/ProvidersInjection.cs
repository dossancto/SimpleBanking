using SimpleBanking.DependencyInjection.Others;
using SimpleBanking.DependencyInjection.Providers;

namespace SimpleBanking.DependencyInjection;

public static class ProvidersInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
      => services
            .AddCommunication()
            .AddUnitOfWork()
            .AddMediator()
            .AddFluentValidationConfiguration()
            .AddIdGenerator()
            .AddHasherProvider()
            .AddTransferAuthorization()
            .AddMessageBroker()
      ;
}
