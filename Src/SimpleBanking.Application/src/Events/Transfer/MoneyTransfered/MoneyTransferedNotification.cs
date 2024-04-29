using MediatR;
using SimpleBanking.Domain.Features.Accounts.Entities;

namespace SimpleBanking.Application.Events.Transfer.MoneyTransfered;

public class MoneyTransferedNotification : INotification
{
    public required Account Sender { get; set; }
    public required Account Receiver { get; set; }

    public required int Ammount { get; set; }
}
