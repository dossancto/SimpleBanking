using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBanking.API.Controllers;

/// <summary>
/// Controller responsible for handling operations related to Persons.
/// </summary>
[ApiController]
[Route("[controller]")]
public partial class PersonsController
(
    IMediator _mediator
 )
: ControllerBase
{ }
