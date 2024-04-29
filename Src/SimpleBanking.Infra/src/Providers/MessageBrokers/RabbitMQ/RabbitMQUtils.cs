using System.Text;
using Newtonsoft.Json;
using SimpleBanking.Domain.Providers.Exceptions;
using SimpleBanking.Domain.Providers.Queues;

namespace SimpleBanking.Infra.Providers.MessageBrokers.RabbitMQ;

public static class RabbitMQUtils
{
    public static ReadOnlyMemory<byte> JsonSerializeData(this object data)
    {
        var jsonContent = JsonConvert.SerializeObject(data);

        var bytes = Encoding.UTF8.GetBytes(jsonContent);

        return bytes;
    }

    public static string ExchangeName(this QueuePipeline queue)
    => queue switch
    {
        QueuePipeline.MONEY_TRANSFERED => "transfer-notification-ex",

        _ => throw new QueueException("This queue is not supported")
        {
            Type = QueueExceptionType.QUEUE_NOT_FOUND
        },
    };

    public static string RoutingKey(this QueuePipeline queue)
    => queue switch
    {
        QueuePipeline.MONEY_TRANSFERED => "",

        _ => throw new QueueException("This queue is not supported")
        {
            Type = QueueExceptionType.QUEUE_NOT_FOUND
        },
    };

    public static string QueueName(this QueuePipeline queue)
    => queue switch
    {
        QueuePipeline.MONEY_TRANSFERED => "transfer-notification",

        _ => throw new QueueException("This queue is not supported")
        {
            Type = QueueExceptionType.QUEUE_NOT_FOUND
        },
    };

    public static (string Exchange, string RoutingKey) QueueInfos(this QueuePipeline queue)
      => (
          queue.ExchangeName(),
          queue.RoutingKey()
        );

}

