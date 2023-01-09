using DDDPratica.Catalogo.Domain.Produto;
using DDDPratica.Catalogo.Infrastructure.Data.Context;
using DDDPratica.Catalogo.Infrastructure.Data.Repositories;
using DDDPratica.Core.RegisterServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Catalogo.Infrastructure.RegisterServices;

public class InfrastructureServicesInstaller : IServicesInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        
        services.AddScoped<CatalogoContext>(); 
        services.AddDbContext<CatalogoContext>(options =>
        {
            options.UseSqlServer(""); 
        }); 
        
    }
}