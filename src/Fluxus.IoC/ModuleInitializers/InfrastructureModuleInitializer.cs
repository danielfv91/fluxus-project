using Fluxus.Application.Domain.Repositories;
using Fluxus.ORM;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Fluxus.ORM.Repositories;
using Fluxus.Application.Domain.UnitOfWork;
using Fluxus.ORM.UnitOfWork;

namespace Fluxus.IoC.ModuleInitializers
{
    public class InfrastructureModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IDailyConsolidationRepository, DailyConsolidationRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
