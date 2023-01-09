namespace DDDPratica.Core.DomainObjects.Entidades;

public abstract class Entidade
{
    public Guid Id { get; private set; }

    protected Entidade()
    {
        Id = Guid.NewGuid(); 
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
}