using DDDPratica.Core.DomainObjects.Eventos;

namespace DDDPratica.Core.Bus;

public interface IMediatorHandler
{
    Task PublicarEvento<T>(T evento) where T : Event; 
}