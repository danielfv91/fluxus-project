using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport;
using Fluxus.Common.Reporting.Interfaces;
using Fluxus.Common.Reporting.Models;
using Fluxus.UnitTests.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;
using QuestPDF.Infrastructure;

namespace Fluxus.UnitTests.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport
{
    public class GenerateDailyCashFlowReportHandlerTests
    {
        private readonly IDailyConsolidationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPdfReportGenerator _pdfReportGenerator;
        private readonly GenerateDailyCashFlowReportHandler _handler;

        public GenerateDailyCashFlowReportHandlerTests()
        {
            _repository = Substitute.For<IDailyConsolidationRepository>();
            _mapper = Substitute.For<IMapper>();
            _pdfReportGenerator = Substitute.For<IPdfReportGenerator>();
            _handler = new GenerateDailyCashFlowReportHandler(_repository, _pdfReportGenerator, _mapper);
        }

        [Fact]
        public async Task Should_Generate_Report_With_Filtered_Data()
        {
            // Arrange
            var consolidations = GenerateDailyCashFlowReportTestData.GenerateConsolidations();
            var query = GenerateDailyCashFlowReportTestData.CreateQueryWithRange();
            var mappedItems = GenerateDailyCashFlowReportTestData.GenerateMappedReportModels();

            _repository.GetAllByUserAsync(query.UserId, Arg.Any<CancellationToken>())
                .Returns(consolidations);

            _mapper.Map<List<DailyCashFlowReportModel>>(Arg.Any<List<Domain.Entities.DailyConsolidation>>())
                .Returns(mappedItems);

            _pdfReportGenerator.GenerateAsync(Arg.Any<IDocument>(), Arg.Any<CancellationToken>())
                .Returns(new byte[] { 1, 2, 3 });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.FileContent.Should().NotBeEmpty();
            result.FileName.Should().Be("fluxo-caixa-diario.pdf");
            result.ContentType.Should().Be("application/pdf");
        }
    }
}
