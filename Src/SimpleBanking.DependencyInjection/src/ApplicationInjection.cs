using SimpleBanking.DependencyInjection.Others;
using SimpleBanking.DependencyInjection.Providers;
using SimpleBanking.DependencyInjection.UseCases;

namespace SimpleBanking.DependencyInjection;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
      => services
            .AddEnvironment()
            .AddDatabase()
            .AddRepositories()
            .AddProviders()
            .AddUseCasesFromAssemblyContaining<Application.Application>()
      ;
}
