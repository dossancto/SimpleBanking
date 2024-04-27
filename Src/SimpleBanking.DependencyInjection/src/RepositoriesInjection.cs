using SimpleBanking.DependencyInjection.Repositories;

namespace SimpleBanking.DependencyInjection;

public static class RepositoriesInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    => services
            .AddPersonRepositories()
    ;

}

