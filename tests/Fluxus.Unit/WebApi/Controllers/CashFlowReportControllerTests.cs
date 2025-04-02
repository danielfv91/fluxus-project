using Xunit;
using Moq;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fluxus.WebApi.Features.CashFlows.Reports;
using Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport;
using Fluxus.Common.Security.Interfaces;
using Fluxus.Common.Reporting.Constants;
using Fluxus.WebApi.Features.CashFlows.DailyCashFlow.Reports;

namespace Fluxus.Unit.WebApi.Controllers;

public class CashFlowReportControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IAuthenticatedUser> _userMock;
    private readonly CashFlowReportController _controller;

    public CashFlowReportControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _userMock = new Mock<IAuthenticatedUser>();
        _controller = new CashFlowReportController(_mediatorMock.Object, _userMock.Object);
    }

    [Fact]
    public async Task Export_ShouldReturnFile_WhenSuccess()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new ExportDailyCashFlowReportRequest
        {
            DateFrom = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
            DateTo = DateOnly.FromDateTime(DateTime.Today)
        };

        var result = new GenerateDailyCashFlowReportResult
        {
            FileContent = new byte[] { 1, 2, 3 },
            ContentType = "application/pdf",
            FileName = "relatorio.pdf"
        };

        _userMock.Setup(x => x.Id).Returns(userId);
        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerateDailyCashFlowReportQuery>(), default))
                     .ReturnsAsync(result);

        // Act
        var response = await _controller.Export(request, default);

        // Assert
        response.Should().BeOfType<FileContentResult>();
        var file = response as FileContentResult;
        file!.ContentType.Should().Be("application/pdf");
        file.FileDownloadName.Should().Be("relatorio.pdf");
        file.FileContents.Should().BeEquivalentTo(result.FileContent);
    }

    [Fact]
    public async Task Export_ShouldReturn504_WhenTimeout()
    {
        // Arrange
        _userMock.Setup(x => x.Id).Returns(Guid.NewGuid());
        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerateDailyCashFlowReportQuery>(), default))
                     .ThrowsAsync(new TaskCanceledException());

        var request = new ExportDailyCashFlowReportRequest
        {
            DateFrom = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
            DateTo = DateOnly.FromDateTime(DateTime.Today)
        };

        // Act
        var response = await _controller.Export(request, default) as ObjectResult;

        // Assert
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(504);
        response.Value.Should().BeEquivalentTo(new
        {
            success = false,
            message = ReportMessages.TimeoutError
        });
    }

    [Fact]
    public async Task Export_ShouldReturn500_WhenUnhandledError()
    {
        // Arrange
        _userMock.Setup(x => x.Id).Returns(Guid.NewGuid());
        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerateDailyCashFlowReportQuery>(), default))
                     .ThrowsAsync(new Exception("Algo deu errado"));

        var request = new ExportDailyCashFlowReportRequest
        {
            DateFrom = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
            DateTo = DateOnly.FromDateTime(DateTime.Today)
        };

        // Act
        var response = await _controller.Export(request, default) as ObjectResult;

        // Assert
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(500);
        response.Value.Should().BeEquivalentTo(new
        {
            success = false,
            message = ReportMessages.UnexpectedError,
            detail = "Algo deu errado"
        });
    }
}
