using MediatR;

namespace DDDPratica.Core.Mensagens.CommonMessages.Notifications;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private List<DomainNotification> _notifications = new List<DomainNotification>(); 
    
    public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
    {
        _notifications.Add(notification);

        return Task.CompletedTask; 
    }
    public virtual IReadOnlyCollection<DomainNotification> ObterNotificacoes() => _notifications;
    public virtual bool TemNotificacao() => ObterNotificacoes().Any();
    public void Clear() => _notifications.Clear();
}