using Bogus;
using Fluxus.Application.Features.Auth.AuthenticateUser;
using Fluxus.Domain.Entities;

namespace Fluxus.UnitTests.Application.TestData
{
    public static class AuthenticateUserHandlerTestData
    {
        private static readonly Faker Faker = new("pt_BR");

        public static AuthenticateUserCommand GenerateValidCommand(string? email = null, string? password = null)
        {
            return new AuthenticateUserCommand
            {
                Email = email ?? Faker.Internet.Email(),
                Password = password ?? Faker.Internet.Password()
            };
        }

        public static User GenerateUser(string email, string passwordHash)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = email,
                PasswordHash = passwordHash
            };
        }
    }
}
