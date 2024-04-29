using MediatR;
using SimpleBanking.Adapters.MessageBrokers;
using SimpleBanking.Domain.Providers.Queues;

namespace SimpleBanking.Application.Events.Transfer.MoneyTransfered;

public class MoneyTransferedHandler(
    IMessageBrokerProvider _messageBroker
    ) : INotificationHandler<MoneyTransferedNotification>
{
    public async Task Handle(MoneyTransferedNotification notification, CancellationToken cancellationToken)
    {
        await _messageBroker.PublishToQueue(QueuePipeline.MONEY_TRANSFERED, notification);
        // await _emailSender.NotifyTranfer(new()
        // {
        //     Ammount = notification.Ammount,
        //     Receiver = notification.Receiver,
        //     Sender = notification.Sender
        // });
    }
}

