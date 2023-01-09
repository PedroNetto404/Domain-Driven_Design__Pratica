using DDDPratica.Core.Communication.Mediator;
using DDDPratica.Core.Mensagens.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDPratica.WebApps.MVC.Controllers;

public class VitrineController : BaseController
{
    public VitrineController
    (
        INotificationHandler<DomainNotification> notificationHandler,
        IMediatorHandler mediatorHandler
    ) : base(notificationHandler, mediatorHandler)
    {
    }
}