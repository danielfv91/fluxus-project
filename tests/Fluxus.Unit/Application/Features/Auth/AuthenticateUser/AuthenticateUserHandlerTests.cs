using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Fluxus.Application.Features.Auth.AuthenticateUser;
using Fluxus.Common.Security.Interfaces;
using Fluxus.Application.Domain.Repositories;
using Fluxus.Domain.Entities;
using Fluxus.UnitTests.Application.TestData;
using NSubstitute;
using Xunit;

namespace Fluxus.UnitTests.Application.Features.Auth.AuthenticateUser
{
    public class AuthenticateUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly AuthenticateUserHandler _handler;

        public AuthenticateUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();

            _handler = new AuthenticateUserHandler(
                _userRepository,
                _passwordHasher,
                _jwtTokenGenerator
            );
        }

        [Fact]
        public async Task Should_Return_Token_When_Credentials_Are_Valid()
        {
            // Arrange
            var command = AuthenticateUserHandlerTestData.GenerateValidCommand();
            var user = AuthenticateUserHandlerTestData.GenerateUser(command.Email, "hashed-password");

            _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
                           .Returns(user);

            _passwordHasher.VerifyPassword(command.Password, user.PasswordHash)
                           .Returns(true);

            _jwtTokenGenerator.GenerateToken(user).Returns("fake-token");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().Be("fake-token");
            result.Email.Should().Be(user.Email);
            result.Name.Should().Be(user.Name);
            result.Id.Should().Be(user.Id);
        }

        [Fact]
        public async Task Should_Throw_When_User_Not_Found()
        {
            // Arrange
            var command = AuthenticateUserHandlerTestData.GenerateValidCommand();

            _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
                           .Returns((User?)null);

            // Act
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                     .WithMessage("Credenciais inválidas.");
        }

        [Fact]
        public async Task Should_Throw_When_Password_Is_Invalid()
        {
            // Arrange
            var command = AuthenticateUserHandlerTestData.GenerateValidCommand();
            var user = AuthenticateUserHandlerTestData.GenerateUser(command.Email, "hashed-password");

            _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
                           .Returns(user);

            _passwordHasher.VerifyPassword(command.Password, user.PasswordHash)
                           .Returns(false);

            // Act
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                     .WithMessage("Credenciais inválidas.");
        }
    }
}
