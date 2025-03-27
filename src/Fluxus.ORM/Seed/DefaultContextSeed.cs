using Fluxus.Common.Security.Interfaces;
using Fluxus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Fluxus.ORM.Seed
{
    public static class DefaultContextSeed
    {
        public static async Task SeedAsync(DefaultContext context, IPasswordHasher passwordHasher, IConfiguration configuration)
        {
            if (!await context.Users.AnyAsync())
            {
                var email = configuration["Seed:AdminEmail"];
                var password = configuration["Seed:AdminPassword"];
                var name = configuration["Seed:AdminName"];
                var role = configuration["Seed:AdminRole"];

                if (string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(role))
                {
                    throw new InvalidOperationException("Admin seed values must be provided via configuration.");
                }

                var admin = new User
                {
                    Name = name,
                    Email = email,
                    PasswordHash = passwordHasher.HashPassword(password),
                    Role = role
                };

                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}
