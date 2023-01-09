using DDDPratica.Core.Mensagens;
using MediatR;

namespace DDDPratica.Core.DomainObjects.Eventos;

public abstract class Event : Message, INotification
{
    public DateTime TimeStamp { get; private set; }

    protected Event()
    {
        TimeStamp = DateTime.Now;
    }
}