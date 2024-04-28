namespace SimpleBanking.Application.Features.Balances.UseCases.Transfer;

public class TransferInput
{
    public required string Sender { get; set; }

    public required string Receiver { get; set; }

    public required int Ammount { get; set; }
}
