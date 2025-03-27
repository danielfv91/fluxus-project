using AutoMapper;
using Fluxus.Application.Features.Transactions.CreateTransaction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fluxus.WebApi.Common;

namespace Fluxus.WebApi.Features.Transactions.CreateTransactionFeature
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TransactionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionRequest request)
        {
            var command = _mapper.Map<CreateTransactionCommand>(request);
            command.UserId = HttpContext.GetUserId();

            var result = await _mediator.Send(command);
            return Ok(_mapper.Map<CreateTransactionResponse>(result));
        }
    }
}
