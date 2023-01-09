using DDDPratica.Core.Mensagens.CommonMessages.DomainEvents;

namespace DDDPratica.Catalogo.Domain.Eventos;

public class ProdutoAbaixoEstoqueEvent : DomainEvent
{
    public int QuantidadeRestante { get; private set; }
    
    public ProdutoAbaixoEstoqueEvent(Guid aggregateId, int quantidadeRestante) : base(aggregateId)
    {
        QuantidadeRestante = quantidadeRestante; 
    }
}