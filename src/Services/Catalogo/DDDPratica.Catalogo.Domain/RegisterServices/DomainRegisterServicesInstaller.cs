using DDDPratica.Catalogo.Domain.Eventos;
using DDDPratica.Catalogo.Domain.Events;
using DDDPratica.Catalogo.Domain.Services;
using DDDPratica.Catalogo.Domain.Servicos;
using DDDPratica.Core.RegisterServices;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Catalogo.Domain.RegisterServices;

public class DomainRegisterServicesInstaller : IServicesInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEstoqueService, EstoqueService>();
        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>(); 
    }
}