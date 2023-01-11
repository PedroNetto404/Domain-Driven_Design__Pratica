using DDDPratica.Vendas.Application.Queries.DTO_s;
using DDDPratica.Vendas.Domain.Pedido.Enums;
using DDDPratica.Vendas.Domain.Pedido.Repositories;

namespace DDDPratica.Vendas.Application.Queries;

public class PedidoQueriesFacade : IPedidoQueriesFacade
{
    private readonly IPedidoRepository _pedidoRepository; 
    
    public async Task<CarrinhoDto> ObterCarrinhoCliente(Guid clientId)
    {
        var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(clientId);
        if (pedido == null) return null;

        var carrinho = new CarrinhoDto
        {
            ClientId = pedido.ClientId,
            ValorTotal = pedido.ValorTotal,
            PedidoId = pedido.Id,
            ValorDesconto = pedido.Desconto,
            SubTotal = pedido.SubTotal
        };

        if (pedido.VoucherId != null) 
            carrinho.VoucherCodigo = pedido.Voucher.Codigo;

        foreach (var item in pedido.PedidoItems)
        {
            carrinho.Items.Add(new CarrinhoItemDto
            {
                ProdutoId = item.ProdutoId,
                ProdutoNome = item.ProdutoNome, 
                Quantidade = item.Quantidade, 
                ValorUnitario = item.ValorUnitario,
                ValorTotal = item.ValorTotal
            });
        }

        return carrinho; 
    }

    public async Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid clienteId)
    {
        var pedidos = await _pedidoRepository.ObterListaPorClienteId(clienteId);

        pedidos = pedidos
            .Where(p => p.Status == PedidoStatus.Pago || p.Status == PedidoStatus.Cancelado)
            .OrderByDescending(p => p.Codigo);

        if (!pedidos.Any()) return null;

        var pedidosDto = new List<PedidoDto>();

        foreach (var pedido in pedidos)
        {
            pedidosDto.Add(new PedidoDto
            {
                ValorTotal = pedido.ValorTotal, 
                PedidoStatus = (int)pedido.Status, 
                Codigo = pedido.Codigo, 
                DataCadastro = pedido.DataCadastro
            });
        }

        return pedidosDto; 
    }
}