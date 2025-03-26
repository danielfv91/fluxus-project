using Fluxus.Common.Security.Interfaces;
using Fluxus.Common.Security.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fluxus.IoC.ModuleInitializers
{
    public class ApplicationModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
        }
    }
}
