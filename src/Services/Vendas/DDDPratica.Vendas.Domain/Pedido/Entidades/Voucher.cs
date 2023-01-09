using DDDPratica.Core.DomainObjects.Entidades;
using DDDPratica.Vendas.Domain.Pedido.Enums;

namespace DDDPratica.Vendas.Domain.Pedido.Entidades;

public class Voucher : Entidade
{
    public string Codigo { get; private set; }
    public decimal? Percentual { get; private set; }
    public decimal? ValorDesconto { get; private set; }
    public int Quantidade { get; private set; }
    public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
    public DateTime DataCricacao { get; private set; }
    public DateTime DataValidade { get; private set; }
    public bool Ativo { get; private set; }
    public bool Utilizado { get; private set; }
    
    //EF Relational
    public ICollection<Pedido> Pedidos { get; set; }

    public Voucher
    (
        string codigo,
        decimal? percentual,
        decimal? valorDesconto,
        int quantidade,
        TipoDescontoVoucher tipoDescontoVoucher,
        DateTime dataCricacao,
        DateTime dataValidade,
        bool ativo,
        bool utilizado
    )
    {
        Codigo = codigo;
        Percentual = percentual;
        ValorDesconto = valorDesconto;
        Quantidade = quantidade;
        TipoDescontoVoucher = tipoDescontoVoucher;
        DataCricacao = dataCricacao;
        DataValidade = dataValidade;
        Ativo = ativo;
        Utilizado = utilizado;
    }
}