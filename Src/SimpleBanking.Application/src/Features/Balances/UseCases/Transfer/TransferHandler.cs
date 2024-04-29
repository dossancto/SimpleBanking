using MediatR;

namespace SimpleBanking.Application.Features.Balances.UseCases.Transfer;

public class TransferHandler(
    TransferUseCase _transfer
    ) : IRequestHandler<TransferCommand, bool>
{

    public async Task<bool> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        await _transfer.Execute(request.Input);

        return true;
    }
}

