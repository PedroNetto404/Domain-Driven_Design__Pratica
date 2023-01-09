using DDDPratica.Core.DomainObjects.Eventos;
using MediatR;

namespace DDDPratica.Core.Bus;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task PublicarEvento<T>(T evento) where T : Event
    {
        await _me
    }
}