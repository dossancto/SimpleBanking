using SimpleBanking.Domain.Providers.Queues;

namespace SimpleBanking.Adapters.MessageBrokers;

public interface IMessageBrokerProvider
{
    /// <summary>
    /// Publish a item to a specific queue
    /// </summary>
    Task PublishToQueue(QueuePipeline queue, object data);

    /// <summary>
    /// Consume a queue
    /// </summary>
    Task Consume(QueuePipeline queue, Func<string, Task<MessageBrokerAckType>> consumeFunc, CancellationToken stoppingToken);
}
