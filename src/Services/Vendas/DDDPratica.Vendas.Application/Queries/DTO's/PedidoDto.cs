namespace DDDPratica.Vendas.Application.Queries.DTO_s;

public class PedidoDto
{
    public int Codigo { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataCadastro { get; set; }
    public int PedidoStatus { get; set; }
}