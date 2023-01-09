using DDDPratica.Core.Communication.Mediator;
using DDDPratica.Core.Mensagens.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDPratica.WebApps.MVC.Controllers;

public abstract class BaseController : Controller
{
    private readonly DomainNotificationHandler _notificationHandler;
    private readonly IMediatorHandler _mediatorHandler;

    protected BaseController
    (
        INotificationHandler<DomainNotification> notificationHandler,
        IMediatorHandler mediatorHandler
    )
    {
        _notificationHandler = (DomainNotificationHandler)notificationHandler;
        _mediatorHandler = mediatorHandler;
    }

    protected bool OperacaoValida()
    {
        return (!_notificationHandler.TemNotificacao());
    }

    protected IEnumerable<DomainNotification> ObterMensagensErro()
    {
        return _notificationHandler.ObterNotificacoes();
    }

    protected void NotificarErro(string codigo, string message)
    {
        _mediatorHandler.PublicarNotificacao
        (
            new DomainNotification
            (
                codigo,
                message
            )
        ); 
    }
}