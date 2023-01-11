using DDDPratica.Vendas.Application.Commands;
using DDDPratica.Vendas.Application.Commands.DTO_s;
using DDDPratica.Vendas.Application.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Vendas.Application.RegisterServices;

public static class VendasApplicationServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
        return services; 
    }
}