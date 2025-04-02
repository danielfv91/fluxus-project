using Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport;
using Fluxus.Common.Reporting.Constants;
using Fluxus.Common.Security.Interfaces;
using Fluxus.WebApi.Features.CashFlows.DailyCashFlow.Reports;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;

namespace Fluxus.WebApi.Features.CashFlows.Reports
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CashFlowReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IMemoryCache _cache;

        public CashFlowReportController(IMediator mediator, IAuthenticatedUser authenticatedUser, IMemoryCache cache)
        {
            _mediator = mediator;
            _authenticatedUser = authenticatedUser;
            _cache = cache;
        }

        [EnableRateLimiting("Relatorio")]
        [HttpPost("daily/report")]
        public async Task<IActionResult> Export([FromBody] ExportDailyCashFlowReportRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _authenticatedUser.Id;
                var cacheKey = $"pdf:{userId}:{request.DateFrom}:{request.DateTo}";

                if (_cache.TryGetValue(cacheKey, out FileContentResult cachedResult))
                    return cachedResult;

                var query = new GenerateDailyCashFlowReportQuery
                {
                    UserId = userId,
                    DateFrom = request.DateFrom,
                    DateTo = request.DateTo
                };

                var result = await _mediator.Send(query, cancellationToken);

                var fileResult = new FileContentResult(result.FileContent, result.ContentType)
                {
                    FileDownloadName = result.FileName
                };

                _cache.Set(cacheKey, fileResult, TimeSpan.FromMinutes(5));

                return fileResult;
            }
            catch (TaskCanceledException)
            {
                return StatusCode(StatusCodes.Status504GatewayTimeout, new
                {
                    success = false,
                    message = ReportMessages.TimeoutError
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = ReportMessages.UnexpectedError,
                    detail = ex.Message
                });
            }
        }
    }
}
