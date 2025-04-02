using Xunit;
using Moq;
using AutoMapper;
using MediatR;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fluxus.WebApi.Features.Transactions;
using Fluxus.Application.Features.Transactions.CreateTransaction;
using Fluxus.Application.Features.Transactions.ListTransactions;
using Fluxus.Application.Features.Consolidations.ListDailyConsolidation;
using Fluxus.WebApi.Features.Transactions.CreateTransactionFeature;
using Fluxus.WebApi.Features.Transactions.ListTransactionsFeature;
using Fluxus.WebApi.Features.Consolidations.ListDailyConsolidation;
using System.Security.Claims;
using Fluxus.WebApi.Common;

namespace Fluxus.Unit.WebApi.Controllers;

public class TransactionsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly TransactionController _controller;

    public TransactionsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _mapperMock = new Mock<IMapper>();
        _controller = new TransactionController(_mediatorMock.Object, _mapperMock.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        }));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task Create_ShouldReturnOk_WhenTransactionIsCreated()
    {
        // Arrange
        var request = new CreateTransactionRequest { Amount = 100, Description = "Teste", Type = 0 };
        var command = new CreateTransactionCommand();
        var result = new CreateTransactionResult();
        var response = new CreateTransactionResponse();

        _mapperMock.Setup(m => m.Map<CreateTransactionCommand>(request)).Returns(command);
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTransactionCommand>(), default)).ReturnsAsync(result);
        _mapperMock.Setup(m => m.Map<CreateTransactionResponse>(result)).Returns(response);

        // Act
        var actionResult = await _controller.Create(request);
        var okResult = actionResult as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult!.Value.Should().BeOfType<ApiResponseWithData<CreateTransactionResponse>>();
        var data = okResult.Value as ApiResponseWithData<CreateTransactionResponse>;
        data!.Success.Should().BeTrue();
        data.Data.Should().Be(response);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WithTransactionList()
    {
        // Arrange
        var request = new ListTransactionsRequest();
        var query = new ListTransactionsQuery();
        var result = new List<ListTransactionsResult> { new() };

        _mapperMock.Setup(m => m.Map<ListTransactionsQuery>(request)).Returns(query);
        _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(result);

        // Act
        var actionResult = await _controller.GetAll(request, default);
        var okResult = actionResult as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult!.Value.Should().BeOfType<ApiResponseWithData<List<ListTransactionsResult>>>();
        var data = okResult.Value as ApiResponseWithData<List<ListTransactionsResult>>;
        data!.Success.Should().BeTrue();
        data.Data.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task GetConsolidation_ShouldReturnOk_WithConsolidationData()
    {
        // Arrange
        var request = new ListDailyConsolidationRequest();
        var query = new ListDailyConsolidationQuery();
        var result = new List<ListDailyConsolidationResult> { new() };
        var response = new List<ListDailyConsolidationResponse> { new() };

        _mapperMock.Setup(m => m.Map<ListDailyConsolidationQuery>(request)).Returns(query);
        _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(result);
        _mapperMock.Setup(m => m.Map<IEnumerable<ListDailyConsolidationResponse>>(result)).Returns(response);

        // Act
        var actionResult = await _controller.GetConsolidation(request, default);
        var okResult = actionResult as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult!.Value.Should().BeOfType<ApiResponseWithData<IEnumerable<ListDailyConsolidationResponse>>>();
        var data = okResult.Value as ApiResponseWithData<IEnumerable<ListDailyConsolidationResponse>>;
        data!.Success.Should().BeTrue();
        data.Data.Should().BeEquivalentTo(response);
    }
}
