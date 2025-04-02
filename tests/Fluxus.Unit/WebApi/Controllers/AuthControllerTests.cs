using Xunit;
using Moq;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fluxus.WebApi.Features.Auth;
using Fluxus.WebApi.Features.Auth.AuthenticateUserFeature;
using Fluxus.Application.Features.Auth.AuthenticateUser;
using System.Threading;
using FluentValidation.Results;
using Fluxus.WebApi.Common;

namespace Fluxus.Unit.WebApi.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _mapperMock = new Mock<IMapper>();
        _controller = new AuthController(_mediatorMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AuthenticateUser_ShouldReturnOk_WhenValidRequest()
    {
        // Arrange
        var request = new AuthenticateUserRequest
        {
            Email = "user@email.com",
            Password = "123456"
        };

        var command = new AuthenticateUserCommand();
        var result = new AuthenticateUserResult();
        var response = new AuthenticateUserResponse();

        _mapperMock.Setup(m => m.Map<AuthenticateUserCommand>(request)).Returns(command);
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);
        _mapperMock.Setup(m => m.Map<AuthenticateUserResponse>(result)).Returns(response);

        // Act
        var actionResult = await _controller.AuthenticateUser(request, default);
        var okResult = actionResult as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull("esperamos um OkObjectResult");
        okResult!.Value.Should().NotBeNull("esperamos um corpo de resposta");

        // Desempacotando o nível duplo de resposta
        var outer = okResult.Value as ApiResponseWithData<ApiResponseWithData<AuthenticateUserResponse>>;
        outer.Should().NotBeNull("esperamos um ApiResponseWithData<ApiResponseWithData<T>>");
        outer!.Success.Should().BeTrue();

        var inner = outer.Data;
        inner.Should().NotBeNull();
        inner!.Data.Should().Be(response);
    }

    [Fact]
    public async Task AuthenticateUser_ShouldReturnBadRequest_WhenValidationFails()
    {
        // Arrange
        var request = new AuthenticateUserRequest
        {
            Email = "",
            Password = ""
        };

        // Act
        var result = await _controller.AuthenticateUser(request, default);
        var badRequestResult = result as BadRequestObjectResult;

        // Assert
        badRequestResult.Should().NotBeNull();
        badRequestResult!.StatusCode.Should().Be(400);

        var errors = badRequestResult.Value as IEnumerable<ValidationFailure>;
        errors.Should().NotBeNull();
        errors!.Count().Should().BeGreaterOrEqualTo(2);
        errors.Should().Contain(e => e.PropertyName == "Email");
        errors.Should().Contain(e => e.PropertyName == "Password");
    }
}
