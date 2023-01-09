namespace DDDPratica.Core.Mensagens;

public abstract class Message
{
    public string MessageType { get; protected set; }
    public Guid AggregateId { get; protected set; }

    protected Message()
    {
        MessageType = GetType().Name; //Retorna o nome da classe que implementa a classe Message
    }
}