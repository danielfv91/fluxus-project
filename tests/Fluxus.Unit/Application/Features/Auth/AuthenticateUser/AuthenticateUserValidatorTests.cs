using Bogus;
using FluentAssertions;
using Fluxus.Application.Features.Auth.AuthenticateUser;
using Xunit;

namespace Fluxus.UnitTests.Application.Features.Auth.AuthenticateUser
{
    public class AuthenticateUserValidatorTests
    {
        private readonly AuthenticateUserValidator _validator;
        private readonly Faker _faker;

        public AuthenticateUserValidatorTests()
        {
            _validator = new AuthenticateUserValidator();
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var command = new AuthenticateUserCommand
            {
                Email = string.Empty,
                Password = _faker.Internet.Password()
            };

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Email");
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var command = new AuthenticateUserCommand
            {
                Email = "email-invalido",
                Password = _faker.Internet.Password()
            };

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Email");
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            var command = new AuthenticateUserCommand
            {
                Email = _faker.Internet.Email(),
                Password = string.Empty
            };

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Password");
        }

        [Fact]
        public void Should_Pass_When_Email_And_Password_Are_Valid()
        {
            var command = new AuthenticateUserCommand
            {
                Email = _faker.Internet.Email(),
                Password = _faker.Internet.Password()
            };

            var result = _validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }
    }
}
