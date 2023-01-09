using DDDPratica.Core.DomainObjects.Eventos;

namespace DDDPratica.Core.EventBus;

public interface IEventHandler
{
    Task PublicarEvento<T>(T evento) where T : Event; 
}