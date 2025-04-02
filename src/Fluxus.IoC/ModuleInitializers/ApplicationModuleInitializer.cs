using Fluxus.Common.Security.Interfaces;
using Fluxus.Common.Security.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Fluxus.Common.Reporting.Interfaces;
using Fluxus.Common.Reporting.Services;

namespace Fluxus.IoC.ModuleInitializers
{
    public class ApplicationModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
            builder.Services.AddScoped<IPdfReportGenerator, PdfReportGenerator>();

        }
    }
}
