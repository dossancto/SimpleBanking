using MediatR;

namespace SimpleBanking.Application.Features.Balances.UseCases.Transfer;

public class TransferCommand : IRequest<bool>
{
    public required TransferInput Input { get; set; }
}

