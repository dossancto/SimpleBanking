using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBanking.API.Controllers.Transfers;

/// <summary>
/// Controller responsible for handling operations related to Transfers.
/// </summary>
[ApiController]
[Route("[controller]")]
public partial class TransfersController
(
    IMediator _mediator
 )
: ControllerBase
{ }


