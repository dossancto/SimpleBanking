using MediatR;
using SimpleBanking.Domain.Features.Merchants.Entities;

namespace SimpleBanking.Application.Features.Merchants.UseCases.CreateMerchant;

public class CreateMerchantCommand : IRequest<Merchant>
{
    public required CreateMerchantInput Input { get; set; }
}
