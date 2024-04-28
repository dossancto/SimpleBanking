using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Application.Features.Merchants.UseCases.CreateMerchant;

namespace SimpleBanking.API.Controllers.Merchants;

public partial class MerchantsController
{
    [HttpPost]
    public async Task<IActionResult> CreateMerchant(CreateMerchantInput input)
    {
        var command = new CreateMerchantCommand()
        {
            Input = input
        };

        var result = await _mediator.Send(command);

        return Created("", result);
    }
}

