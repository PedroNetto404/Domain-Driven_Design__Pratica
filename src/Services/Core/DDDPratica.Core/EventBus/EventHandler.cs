using DDDPratica.Core.Mensagens;
using MediatR;

namespace DDDPratica.Core.EventBus;

public class EventHandler : IEventHandler
{
    private readonly IMediator _mediator;

    public EventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublicarEvento<T>(T evento) where T : Event
    {
        await _mediator.Publish(evento);
    }
}