namespace DDDPratica.Core.DomainObjects.Eventos;

public class DomainEvent : Event
{
    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId; 
    }
}