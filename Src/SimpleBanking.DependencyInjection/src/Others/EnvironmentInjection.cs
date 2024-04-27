using DotNetEnv;

namespace SimpleBanking.DependencyInjection.Others;

public static class EnvironmentInjection
{
    public static IServiceCollection AddEnvironment(this IServiceCollection services)
    {
        Env.TraversePath().Load();

        return services;
    }
}

