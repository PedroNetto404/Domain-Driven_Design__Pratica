using DDDPratica.Core.Mensagens;
using DDDPratica.Core.Mensagens.CommonMessages.Notifications;

namespace DDDPratica.Core.Communication.Mediator;

public interface IMediatorHandler
{
    Task PublicarEvento<T>(T evento) where T : Event; 
    Task<bool> EnviarComando<T>(T comando) where T : Command;
    Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification; 
}