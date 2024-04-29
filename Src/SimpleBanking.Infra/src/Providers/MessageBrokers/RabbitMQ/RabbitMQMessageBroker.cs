using SimpleBanking.Adapters.MessageBrokers;
using SimpleBanking.Domain.Providers.Queues;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace SimpleBanking.Infra.Providers.MessageBrokers.RabbitMQ;

public class RabbitMQMessageBroker(ConnectionFactory factory) : IMessageBrokerProvider
{
    public async Task Consume(QueuePipeline queue, Func<string, Task<MessageBrokerAckType>> consumeFunc, CancellationToken stoppingToken)
    {
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var isAct = await consumeFunc(message);

            var multiple = false;
            var tag = ea.DeliveryTag;

            switch (isAct)
            {
                case (MessageBrokerAckType.ACK):
                    channel.BasicAck(deliveryTag: tag, multiple: multiple);
                    break;

                case (MessageBrokerAckType.NACK):
                    channel.BasicNack(deliveryTag: tag, multiple: multiple, requeue: false);
                    break;

                case (MessageBrokerAckType.RETRY):
                    channel.BasicReject(deliveryTag: tag, requeue: true);
                    break;

                case (MessageBrokerAckType.REJECT):
                    channel.BasicReject(deliveryTag: tag, requeue: false);
                    break;
            }
        };

        channel.BasicConsume(queue: queue.QueueName(), autoAck: false, consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public Task PublishToQueue(QueuePipeline queue, object data)
    {
        var exchange = queue.ExchangeName();
        var routingKeys = queue.RoutingKey();

        PublishToExchange(exchange, routingKeys, data);

        return Task.CompletedTask;
    }

    private void PublishToExchange(string exchange, string routingKey, object data)
    {
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var body = data.JsonSerializeData();

        channel.BasicPublish(exchange: exchange,
                             routingKey: routingKey,
                             basicProperties: null,
                             body: body);
    }
}
