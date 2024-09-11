using Edupocket.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Edupocket.Api.Controllers.v1
{
    [Route("v{version:apiVersion}/wallet")]
    [ApiController]
    [ApiVersion("1.0")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [RequireHttps]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletCommand model)
        {
            var response = await _mediator.Send(model);
            return Ok(response);
        }
    }
}
