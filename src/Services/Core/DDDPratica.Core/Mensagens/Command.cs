using FluentValidation.Results;
using MediatR;

namespace DDDPratica.Core.Mensagens;

public class Command : Message, IRequest<bool>
{
    public DateTime TimeSpan { get; private set; }
    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {
        TimeSpan = DateTime.Now; 
    }
    public virtual bool EhValido()
    {
        throw new NotImplementedException();
    }
}