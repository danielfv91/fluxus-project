using Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport;
using Fluxus.Common.Reporting.Constants;
using Fluxus.Common.Security.Interfaces;
using Fluxus.WebApi.Features.CashFlows.DailyCashFlow.Reports;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fluxus.WebApi.Features.CashFlows.Reports
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CashFlowReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthenticatedUser _authenticatedUser;

        public CashFlowReportController(IMediator mediator, IAuthenticatedUser authenticatedUser)
        {
            _mediator = mediator;
            _authenticatedUser = authenticatedUser;
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export([FromQuery] ExportDailyCashFlowReportRequest request, CancellationToken cancellationToken)
        {
            var query = new GenerateDailyCashFlowReportQuery
            {
                UserId = _authenticatedUser.Id,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo
            };

            var result = await _mediator.Send(query, cancellationToken);

            return new FileContentResult(result.FileContent, result.ContentType)
            {
                FileDownloadName = result.FileName
            };
        }
    }
}
