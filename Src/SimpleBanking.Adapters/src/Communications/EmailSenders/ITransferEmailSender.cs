using SimpleBanking.Domain.Features.Accounts.Entities;

namespace SimpleBanking.Adapters.Communications.EmailSenders;

public interface ITransferEmailSender
{
    Task NotifyTranfer(NotifyTranferInput input);
}

public class NotifyTranferInput
{
    public required Account Sender { get; set; }
    public required Account Receiver { get; set; }
    public required int Ammount { get; set; }

    public DateTime TransferedAt { get; set; } = DateTime.UtcNow;
}
