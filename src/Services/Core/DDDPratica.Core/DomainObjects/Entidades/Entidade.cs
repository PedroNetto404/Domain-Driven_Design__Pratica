using DDDPratica.Core.Mensagens;

namespace DDDPratica.Core.DomainObjects.Entidades;

public abstract class Entidade
{
    private List<Event> _notificacoes;
    public IReadOnlyCollection<Event> Notificacoes => _notificacoes; 
    public Guid Id { get; private set; }

    protected Entidade()
    {
        Id = Guid.NewGuid(); 
    }

    public void AdicionarEvento(Event evento)
    {
        _notificacoes = _notificacoes ?? new List<Event>(); 
        _notificacoes.Add(evento);
    }

    public void RemoverEvento(Event eventItem)
    {
        _notificacoes?.Remove(eventItem);
    }

    public void LimparEventos()
    {
        _notificacoes?.Clear();
    }

    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entidade;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Equals(compareTo); 
    }

    protected bool Equals(Entidade other)
    {
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

    public static bool operator ==(Entidade? left, Entidade? right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

        return left.Equals(right); 
    }

    public static bool operator !=(Entidade left, Entidade right) => !(left == right);

    public override string ToString() => $"{GetType().Name} [Id={Id}]";

    public virtual bool EhValido()
    {
        throw new NotImplementedException(); 
    }
}