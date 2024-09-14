using Edupocket.Application.Commands;
using Edupocket.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Edupocket.Api.Controllers.v1
{
    /// <summary>
    /// Wallet Controller
    /// </summary>
    [Route("v{version:apiVersion}/wallet")]
    [ApiController]
    [ApiVersion("1.0")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// WalletController constructor
        /// </summary>
        /// <param name="mediator"></param>
        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Generates a new wallet for a profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [RequireHttps]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletCommand model)
        {
            var response = await _mediator.Send(model);
            return Ok(response);
        }

        /// <summary>
        /// Gets Wallet by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("Id")]
        public async Task<IActionResult> GetWalletById(Guid Id)
        {
            var response = await _mediator.Send(new GetWalletByIdQuery(Id));
            if(response.IsSuccessful)
                return Ok(response);    

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Gets Wallet by wallet number
        /// </summary>
        /// <param name="walletNumber"></param>
        /// <returns></returns>
        [HttpGet("walletNumber")]
        public async Task<IActionResult> GetWalletByNumber(string walletNumber)
        {
            var response = await _mediator.Send(new GetWalletByWalletNumberQuery(walletNumber));
            if (response.IsSuccessful)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Get paged wallet by filter
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedWallets([FromQuery] GetWalletListQuery query)
        {
            var response = await _mediator.Send(query);
            if(response.IsSuccessful)
                return Ok(response);

            return BadRequest(response.Message);
        }

    }
}
