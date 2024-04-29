using SimpleBanking.Adapters.Communications.EmailSenders;
using SimpleBanking.Infra.Providers.Communications;

namespace SimpleBanking.DependencyInjection.Providers;

internal static class CommunicationInjection
{
    public static IServiceCollection AddCommunication(this IServiceCollection services)
      => services
          .AddTransient<IEmailSenderAdapter, FakeEmailSender>()
      ;
}

