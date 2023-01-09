using DDDPratica.Core.Mensagens;

namespace DDDPratica.Core.EventBus;

public interface IEventHandler
{
    Task PublicarEvento<T>(T evento) where T : Event; 
}