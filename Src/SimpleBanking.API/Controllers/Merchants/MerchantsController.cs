using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBanking.API.Controllers.Merchants;

/// <summary>
/// Controller responsible for handling operations related to Merchants.
/// </summary>
[ApiController]
[Route("[controller]")]
public partial class MerchantsController
(
    IMediator _mediator
 )
: ControllerBase
{ }

