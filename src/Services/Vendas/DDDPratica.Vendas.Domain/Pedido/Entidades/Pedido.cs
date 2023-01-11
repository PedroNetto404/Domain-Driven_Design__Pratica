using DDDPratica.Core.DomainObjects;
using DDDPratica.Core.DomainObjects.Entidades;
using DDDPratica.Core.DomainObjects.Exceptions;
using DDDPratica.Vendas.Domain.Pedido.Enums;

namespace DDDPratica.Vendas.Domain.Pedido.Entidades;

public class Pedido : Entidade, IRaizAgregacao
{
    public int Codigo { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherUtilizado { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal ValorTotal { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public PedidoStatus Status { get; set; }
    public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
    public decimal SubTotal => ValorTotal + Desconto; 
    
    //EF Relational
    public Voucher Voucher { get; private set; }

    private readonly List<PedidoItem> _pedidoItems = new List<PedidoItem>();

    public Pedido
    (
        Guid clientId,
        bool voucherUtilizado,
        decimal desconto,
        decimal valorTotal
    )
    {
        ClientId = clientId;
        VoucherUtilizado = voucherUtilizado;
        Desconto = desconto;
        ValorTotal = valorTotal;
    }

    protected Pedido() { }

    public void CalcularValorTotalDesconto()
    {
        if(!VoucherUtilizado) return;

        decimal desconto = 0;
        var valor = ValorTotal;

        if (Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Percentagem)
        {
            if (Voucher.Percentual.HasValue)
            {
                desconto = (valor * Voucher.Percentual.Value) / 100;
                valor -= desconto; 
            }
        }
        else if (Voucher.ValorDesconto.HasValue)
        {
            desconto = Voucher.ValorDesconto.Value;
            valor -= desconto; 
        }

        ValorTotal = valor < 0 ? 0 : valor;
        Desconto = desconto;
    }

    public void CalcularValorPedido()
    {
        ValorTotal = PedidoItems.Sum(p => p.CalcularValor()); 
        CalcularValorTotalDesconto();
    }

    public void AplicarVoucher(Voucher voucher)
    {
        Voucher = voucher;
        VoucherUtilizado = true; 
        CalcularValorPedido();
    }

    public void AdicionarItem(PedidoItem item)
    {
        if (!item.EhValido()) return; 
        
        item.AssociarPedido(Id);

        if (PedidoItemExistente(item))
        {
            var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId); 
            itemExistente.AdicionarUnidades(item.Quantidade);
            item = itemExistente;

            _pedidoItems.Remove(itemExistente); 
        }

        item.CalcularValor(); 
        _pedidoItems.Add(item);
    }

    public void RemoverItem(PedidoItem item)
    {
        if (!item.EhValido()) return;

        var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

        if (itemExistente == null) 
            throw new DomainException("O item não pertence ao pedido");
        
        _pedidoItems.Remove(itemExistente); 
        
        CalcularValorPedido();
    }

    public bool PedidoItemExistente(PedidoItem pedidoItem)
    {
        return _pedidoItems.Exists(p => p.ProdutoId == pedidoItem.ProdutoId); 
    }

    public void AtualizaItem(PedidoItem item)
    {
        if (!item.EhValido()) return; 
        item.AssociarPedido(Id);

        var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

        if (itemExistente == null)
            throw new DomainException("O item não pertence ao pedido");

        _pedidoItems.Remove(itemExistente); 
        _pedidoItems.Add(item);
        
        CalcularValorPedido();
    }

    public void AtualizarUnidades(PedidoItem item, int unidades)
    {
        item.AtualizarUnidades(unidades);
        AtualizaItem(item);
    }

    public void TornarRascunho()
    {
        Status = PedidoStatus.Rascunho; 
    }

    public void IniciarPedido()
    {
        Status = PedidoStatus.Iniciado; 
    }

    public void CancelarPedido()
    {
        Status = PedidoStatus.Cancelado; 
    }
    
    public static class PedidoFactory
    {
        public static Pedido NovoPedidoRascunho(Guid clientId)
        {
            var pedido = new Pedido
            {
                ClientId = clientId
            }; 
            
            pedido.TornarRascunho();
            return pedido;
        }
    }
}