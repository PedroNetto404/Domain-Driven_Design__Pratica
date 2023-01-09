using DDDPratica.Core.Mensagens.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDPratica.WebApps.MVC.Extensions;

public class SummaryViewComponent : ViewComponent
{
    private readonly DomainNotificationHandler _notification;

    public SummaryViewComponent(INotificationHandler<DomainNotification> notification)
    {
        _notification ??= notification as DomainNotificationHandler;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var notificacoes = await Task.FromResult((_notification.ObterNotificacoes()));

        foreach (var notification in notificacoes)
        {
            ViewData.ModelState.AddModelError(string.Empty, notification.Value);
        }

        return View(); 
    }
}