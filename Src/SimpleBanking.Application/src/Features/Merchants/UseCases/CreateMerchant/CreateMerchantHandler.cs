using MediatR;
using SimpleBanking.Domain.Features.Merchants.Entities;

namespace SimpleBanking.Application.Features.Merchants.UseCases.CreateMerchant;

public class CreateMerchantHandler(
    CreateMerchantUseCase _createMerchant
    ) : IRequestHandler<CreateMerchantCommand, Merchant>
{
    public Task<Merchant> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
    {
        return _createMerchant.Execute(request.Input);
    }
}
