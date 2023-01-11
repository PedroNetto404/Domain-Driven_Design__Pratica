namespace DDDPratica.Vendas.Application.Queries.DTO_s;

public class CarrinhoDto
{
    public Guid PedidoId { get; set; }
    public Guid ClientId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal ValorTotal { get; set; }
    public decimal ValorDesconto { get; set; }
    public string VoucherCodigo { get; set; }
    public List<CarrinhoItemDto> Items { get; set; } = new(); 
    public CarrinhoPagamentoDto Pagamento { get; set; }
}