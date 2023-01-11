using DDDPratica.Catalogo.Domain.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Catalogo.Domain.RegisterServices;

public static class CatalogoDomainServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEstoqueService, EstoqueService>(); 
        
        return services;
    }
}