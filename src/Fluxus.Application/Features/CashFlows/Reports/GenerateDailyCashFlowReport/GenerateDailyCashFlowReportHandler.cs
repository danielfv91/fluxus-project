using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using Fluxus.Common.Reporting.Constants;
using Fluxus.Common.Reporting.Interfaces;
using Fluxus.Common.Reporting.Models;
using Fluxus.Common.Reporting.Services.Documents;
using MediatR;

namespace Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport
{
    public class GenerateDailyCashFlowReportHandler
        : IRequestHandler<GenerateDailyCashFlowReportQuery, GenerateDailyCashFlowReportResult>
    {
        private readonly IDailyConsolidationRepository _repository;
        private readonly IPdfReportGenerator _reportGenerator;
        private readonly IMapper _mapper;

        public GenerateDailyCashFlowReportHandler(
            IDailyConsolidationRepository repository,
            IPdfReportGenerator reportGenerator,
            IMapper mapper)
        {
            _repository = repository;
            _reportGenerator = reportGenerator;
            _mapper = mapper;
        }

        public async Task<GenerateDailyCashFlowReportResult> Handle(
            GenerateDailyCashFlowReportQuery request,
            CancellationToken cancellationToken)
        {
            var consolidations = await _repository.GetAllByUserAsync(request.UserId, cancellationToken);

            if (request.DateFrom.HasValue)
                consolidations = consolidations.Where(x => x.Date >= request.DateFrom.Value);

            if (request.DateTo.HasValue)
                consolidations = consolidations.Where(x => x.Date <= request.DateTo.Value);

            var sortedConsolidations = consolidations.OrderBy(x => x.Date).ToList();

            var items = _mapper.Map<List<DailyCashFlowReportModel>>(sortedConsolidations);

            decimal accumulated = 0;
            foreach (var item in items)
            {
                accumulated += item.DailyBalance;
                item.AccumulatedBalance = accumulated;
            }

            var document = new DailyCashFlowReportDocument(items);

            var pdfBytes = await _reportGenerator.GenerateAsync(document, cancellationToken);

            return new GenerateDailyCashFlowReportResult
            {
                FileName = ReportMetadata.DailyCashFlowFileName,
                FileContent = pdfBytes,
                ContentType = ReportMetadata.PdfContentType
            };
        }
    }
}
