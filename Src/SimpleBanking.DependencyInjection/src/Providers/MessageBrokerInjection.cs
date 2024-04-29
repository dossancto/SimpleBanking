using RabbitMQ.Client;
using SimpleBanking.Adapters.MessageBrokers;
using SimpleBanking.Infra.Providers.MessageBrokers.RabbitMQ;
using static SimpleBanking.Infra.Utils.Anvs.Anv;

namespace SimpleBanking.DependencyInjection.Providers;

internal static class MessageBrokerInjection
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services)
      => services
          .AddRabbitMQ()
      ;

    private static IServiceCollection AddRabbitMQ(this IServiceCollection services)
    {
        var factory = new ConnectionFactory()
        {
            HostName = RabbitMQAnv.HOST.NotNull(),
            Port = int.Parse(RabbitMQAnv.PORT.NotNull()),
            VirtualHost = RabbitMQAnv.VHOST.OrDefault("/"),
            UserName = RabbitMQAnv.USER.NotNull(),
            Password = RabbitMQAnv.PASSWORD.NotNull(),
        };

        if (RabbitMQAnv.VHOST.IsDefined() && RabbitMQAnv.VHOST.Value != "/")
        {
            var uri = $"amqps://{RabbitMQAnv.USER.Value}:{RabbitMQAnv.PASSWORD.Value}@{RabbitMQAnv.HOST.Value}/{RabbitMQAnv.VHOST.OrDefault("")}";
            factory.Uri = new(uri);
        }

        services.AddScoped(_ => factory);

        services.AddScoped<IMessageBrokerProvider, RabbitMQMessageBroker>();

        return services;
    }
}

