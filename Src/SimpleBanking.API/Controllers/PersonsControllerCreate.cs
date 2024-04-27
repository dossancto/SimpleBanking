using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Application.Features.Persons.UseCases.CreatePerson;

namespace SimpleBanking.API.Controllers;

public partial class PersonsController
{
    [HttpPost]
    public async Task<IActionResult> CreatePerson(CreatePersonInput input)
    {
        var command = new CreatePersonCommmand()
        {
            Input = input
        };

        var result = await _mediator.Send(command);

        return Created("", result);
    }
}

