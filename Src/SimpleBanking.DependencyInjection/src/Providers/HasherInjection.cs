using SimpleBanking.Adapters.Hash;
using SimpleBanking.Infra.Providers.Hasher;

namespace SimpleBanking.DependencyInjection.Providers;

internal static class HasherInjection
{
    public static IServiceCollection AddHasherProvider(this IServiceCollection services)
      => services
            .AddScoped<IPasswordHasher, IFakeHasher>()
      ;
}
