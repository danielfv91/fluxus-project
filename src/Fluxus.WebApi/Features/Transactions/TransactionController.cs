using AutoMapper;
using Fluxus.Application.Features.Consolidations.ListDailyConsolidation;
using Fluxus.Application.Features.Transactions.CreateTransaction;
using Fluxus.Application.Features.Transactions.ListTransactions;
using Fluxus.WebApi.Common;
using Fluxus.WebApi.Features.Consolidations.ListDailyConsolidation;
using Fluxus.WebApi.Features.Transactions.CreateTransactionFeature;
using Fluxus.WebApi.Features.Transactions.ListTransactionsFeature;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneOf.Types;

namespace Fluxus.WebApi.Features.Transactions
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
        public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request)
        {
            var command = _mapper.Map<CreateTransactionCommand>(request);
            command.UserId = HttpContext.GetUserId();

            var result = await _mediator.Send(command);
            var response = _mapper.Map<CreateTransactionResponse>(result);

            return Ok(new ApiResponseWithData<CreateTransactionResponse>
            {
                Success = true,
                Message = "Transação criada com sucesso.",
                Data = response
            });
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<List<ListTransactionsResult>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] ListTransactionsRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<ListTransactionsQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(new ApiResponseWithData<List<ListTransactionsResult>>
            {
                Success = true,
                Message = "Transações listadas com sucesso.",
                Data = result
            });
        }

        [HttpGet("consolidation")]
        [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<ListDailyConsolidationResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetConsolidation([FromQuery] ListDailyConsolidationRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<ListDailyConsolidationQuery>(request);
            query.UserId = HttpContext.GetUserId();

            var result = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<IEnumerable<ListDailyConsolidationResponse>>(result);

            return Ok(new ApiResponseWithData<IEnumerable<ListDailyConsolidationResponse>>
            {
                Success = true,
                Message = "Consolidação listada com sucesso.",
                Data = response
            });
        }

    }
}
