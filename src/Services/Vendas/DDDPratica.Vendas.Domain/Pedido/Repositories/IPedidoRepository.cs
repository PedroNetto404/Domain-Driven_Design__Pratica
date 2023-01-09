using DDDPratica.Core.Data;
using DDDPratica.Vendas.Domain.Pedido.Entidades;

namespace DDDPratica.Vendas.Domain.Pedido.Repositories;

public interface IPedidoRepository : IRepository<Entidades.Pedido>
{
    Task<IEnumerable<Entidades.Pedido>> ObterListaPorClienteId(Guid clientId);
    Task<Entidades.Pedido> ObterPedidoRascunhoPorClienteId(Guid clientId);
    void Atualizar(Entidades.Pedido pedido);
    void Adicionar(Entidades.Pedido pedido);
    Task<PedidoItem> ObterItemPorId(Guid id);
    Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
    void AdicionarItem(PedidoItem pedidoItem);
    void AtualizarItem(PedidoItem pedidoItem);
    void RemoverItem(PedidoItem pedidoItem);
    Task<Voucher> ObterVoucherPorCodigo(string codigo); 
} 