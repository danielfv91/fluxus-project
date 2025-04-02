using Xunit;
using Moq;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fluxus.WebApi.Features.CashFlows.Reports;
using Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport;
using Fluxus.Common.Security.Interfaces;
using Fluxus.Common.Reporting.Constants;
using Microsoft.Extensions.Caching.Memory;
using Fluxus.WebApi.Features.CashFlows.DailyCashFlow.Reports;

namespace Fluxus.Unit.WebApi.Controllers;

public class CashFlowReportControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IAuthenticatedUser> _userMock;
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly CashFlowReportController _controller;

    public CashFlowReportControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _userMock = new Mock<IAuthenticatedUser>();
        _cacheMock = new Mock<IMemoryCache>();
        _controller = new CashFlowReportController(_mediatorMock.Object, _userMock.Object, _cacheMock.Object);
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
        _cacheMock.Setup(c => c.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny)).Returns(false);
        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerateDailyCashFlowReportQuery>(), default))
                     .ReturnsAsync(result);

        var cacheEntry = new Mock<ICacheEntry>();
        _cacheMock.Setup(c => c.CreateEntry(It.IsAny<object>())).Returns(cacheEntry.Object);

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
        _cacheMock.Setup(c => c.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny)).Returns(false);
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
        _cacheMock.Setup(c => c.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny)).Returns(false);
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

    [Fact]
    public async Task Export_ShouldReturnCachedResult_WhenExistsInCache()
    {
        // Arrange
        var request = new ExportDailyCashFlowReportRequest
        {
            DateFrom = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
            DateTo = DateOnly.FromDateTime(DateTime.Today)
        };

        var userId = Guid.NewGuid();
        var expectedResult = new FileContentResult(new byte[] { 1, 2, 3 }, "application/pdf")
        {
            FileDownloadName = "relatorio.pdf"
        };

        object dummyOut = expectedResult;

        _userMock.Setup(u => u.Id).Returns(userId);
        _cacheMock.Setup(c => c.TryGetValue(It.IsAny<object>(), out dummyOut)).Returns(true);

        // Act
        var result = await _controller.Export(request, default);

        // Assert
        result.Should().BeSameAs(expectedResult);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GenerateDailyCashFlowReportQuery>(), default), Times.Never);
    }

    [Fact]
    public async Task Export_ShouldCacheResult_WhenGenerated()
    {
        // Arrange
        var request = new ExportDailyCashFlowReportRequest
        {
            DateFrom = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)),
            DateTo = DateOnly.FromDateTime(DateTime.Today.AddDays(-1))
        };

        var userId = Guid.NewGuid();
        var result = new GenerateDailyCashFlowReportResult
        {
            FileContent = new byte[] { 9, 8, 7 },
            ContentType = "application/pdf",
            FileName = "relatorio_gerado.pdf"
        };

        _userMock.Setup(u => u.Id).Returns(userId);
        _cacheMock.Setup(c => c.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny)).Returns(false);
        _mediatorMock.Setup(m => m.Send(It.IsAny<GenerateDailyCashFlowReportQuery>(), default)).ReturnsAsync(result);

        var cacheEntryMock = new Mock<ICacheEntry>();
        _cacheMock.Setup(c => c.CreateEntry(It.IsAny<object>())).Returns(cacheEntryMock.Object);

        // Act
        var response = await _controller.Export(request, default);

        // Assert
        var fileResult = response as FileContentResult;
        fileResult.Should().NotBeNull();
        fileResult!.FileContents.Should().BeEquivalentTo(result.FileContent);
        fileResult.ContentType.Should().Be(result.ContentType);
        fileResult.FileDownloadName.Should().Be(result.FileName);

        _cacheMock.Verify(c => c.CreateEntry(It.IsAny<object>()), Times.Once);
    }
}
