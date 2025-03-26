using Microsoft.AspNetCore.Builder;

namespace Fluxus.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
