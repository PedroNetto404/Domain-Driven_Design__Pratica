using DDDPratica.Vendas.Application.Queries.DTO_s;

namespace DDDPratica.Vendas.Application.Queries;

public interface IPedidoQueriesFacade
{
    Task<CarrinhoDto> ObterCarrinhoCliente(Guid clientId);
    Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid clienteId); 
}