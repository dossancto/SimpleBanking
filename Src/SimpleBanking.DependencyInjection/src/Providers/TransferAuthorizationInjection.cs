using SimpleBanking.Adapters.Transfering;
using SimpleBanking.Infra.Providers.Transfering;

namespace SimpleBanking.DependencyInjection.Providers;

internal static class TransferAuthorizationInjection
{
    public static IServiceCollection AddTransferAuthorization(this IServiceCollection services)
      => services
          .AddTransient<ITransferAuthorizerAdapter, MockTransferAuthorization>()
      ;
}
