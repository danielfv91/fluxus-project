using AutoMapper;
using Fluxus.Application.Features.CashFlows.DailyCashFlow;
using Fluxus.WebApi.Common;
using Fluxus.WebApi.Features.CashFlows.DailyCashFlow;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fluxus.WebApi.Features.CashFlows
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CashFlowController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CashFlowController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("daily")]
        [ProducesResponseType(typeof(ApiResponseWithData<List<DailyCashFlowResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDailyCashFlow([FromQuery] DailyCashFlowRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<DailyCashFlowQuery>(request);
            query.UserId = HttpContext.GetUserId();

            var result = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<List<DailyCashFlowResponse>>(result);

            return Ok(new ApiResponseWithData<List<DailyCashFlowResponse>>
            {
                Success = true,
                Message = "Fluxo de caixa diário obtido com sucesso.",
                Data = response
            });
        }
    }
}
