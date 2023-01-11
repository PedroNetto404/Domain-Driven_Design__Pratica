namespace DDDPratica.Vendas.Application.Queries.DTO_s;

public class CarrinhoItemDto
{
    public Guid ProdutoId { get; set; }
    public string ProdutoNome { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
}