using static SimpleBanking.Infra.Utils.Anvs.Anv;

namespace SimpleBanking.Infra.Utils.Anvs;

public static partial class Anv
{
    public static readonly AnvRabbitMQ RabbitMQAnv = new();
}

public class AnvRabbitMQ
{
    public readonly AnvEnv HOST = Anv.EnvLoad("RABBITMQ_HOST");
    public readonly AnvEnv VHOST = Anv.EnvLoad("RABBITMQ_VHOST");
    public readonly AnvEnv PORT = Anv.EnvLoad("RABBITMQ_PORT");
    public readonly AnvEnv USER = Anv.EnvLoad("RABBITMQ_USER");
    public readonly AnvEnv PASSWORD = Anv.EnvLoad("RABBITMQ_PASSWORD");
}


