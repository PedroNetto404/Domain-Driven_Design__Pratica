using DDDPratica.Core.RegisterServices;
using DDDPratica.Vendas.Application.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Vendas.Application.RegisterServices;

public class ApplicationServicesInstaller : IServicesInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>(); 
    }
}