namespace DDDPratica.Core.Mensagens.CommonMessages.DomainEvents;

public class DomainEvent : Event
{
    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId; 
    }
}