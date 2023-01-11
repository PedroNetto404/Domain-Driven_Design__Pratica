using DDDPratica.Core.Communication.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Core.RegisterServices;

public static class CoreServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>(); 
        return services; 
    }
}