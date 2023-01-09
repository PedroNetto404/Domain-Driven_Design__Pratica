using DDDPratica.Core.RegisterServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Catalogo.Application.RegisterServices;

public class ApplicationServicesInstaller : IServicesInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(ApplicationServicesInstaller).Assembly); 
    }
}