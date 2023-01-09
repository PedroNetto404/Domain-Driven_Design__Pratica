using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Core.RegisterServices;

public class MediatrServicesInstaller : IServicesInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(MediatrServicesInstaller).Assembly);
    }
}