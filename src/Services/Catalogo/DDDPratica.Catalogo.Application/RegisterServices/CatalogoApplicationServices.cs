using DDDPratica.Catalogo.Application.Services;
using DDDPratica.Catalogo.Application.Serviços;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Catalogo.Application.RegisterServices;

public static class CatalogoApplicationServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProdutoAppService, ProdutoAppService>();

        return services;
    }
}