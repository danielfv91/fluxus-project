using Xunit;
using Moq;
using FluentAssertions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fluxus.WebApi.Features.CashFlows;
using Fluxus.Application.Features.CashFlows.DailyCashFlow;
using Fluxus.WebApi.Features.CashFlows.DailyCashFlow;
using System.Security.Claims;
using Fluxus.WebApi.Common;

namespace Fluxus.Unit.WebApi.Controllers;

public class CashFlowControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CashFlowController _controller;

    public CashFlowControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _mapperMock = new Mock<IMapper>();
        _controller = new CashFlowController(_mediatorMock.Object, _mapperMock.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }

    [Fact]
    public async Task GetDailyCashFlow_ShouldReturnOk_WithListOfResponses()
    {
        // Arrange
        var request = new DailyCashFlowRequest
        {
            DateFrom = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
            DateTo = DateOnly.FromDateTime(DateTime.Today)
        };

        var query = new DailyCashFlowQuery();
        var result = new List<DailyCashFlowResult> { new() };
        var response = new List<DailyCashFlowResponse> { new() };

        _mapperMock.Setup(m => m.Map<DailyCashFlowQuery>(request)).Returns(query);
        _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(result);
        _mapperMock.Setup(m => m.Map<List<DailyCashFlowResponse>>(result)).Returns(response);

        // Act
        var actionResult = await _controller.GetDailyCashFlow(request, default);
        var okResult = actionResult as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult!.Value.Should().BeOfType<ApiResponseWithData<List<DailyCashFlowResponse>>>();
        var data = okResult.Value as ApiResponseWithData<List<DailyCashFlowResponse>>;
        data!.Success.Should().BeTrue();
        data.Data.Should().BeEquivalentTo(response);
    }
}
