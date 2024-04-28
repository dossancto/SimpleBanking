using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Application.Features.Balances.UseCases.Transfer;

namespace SimpleBanking.API.Controllers.Transfers;

public partial class TransfersController
{
    [HttpPost]
    public async Task<IActionResult> CreateMerchant(TransferInput input)
    {
        var command = new TransferCommand()
        {
            Input = input
        };

        var result = await _mediator.Send(command);

        return Ok("Your money has been transferred");
    }
}

