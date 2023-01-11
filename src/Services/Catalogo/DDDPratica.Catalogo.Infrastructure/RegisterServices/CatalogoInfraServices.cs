using DDDPratica.Catalogo.Domain.Eventos;
using DDDPratica.Catalogo.Domain.Produto;
using DDDPratica.Catalogo.Infrastructure.Data.Context;
using DDDPratica.Catalogo.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Catalogo.Infrastructure.RegisterServices;

public static class CatalogoInfraServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<CatalogoContext>(); 
        
        return services; 
    }
}