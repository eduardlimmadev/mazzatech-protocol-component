namespace Protocol.Messaging.Interfaces
{
    public interface IEventHandler<in TMessage>
    {
        Task Handle(TMessage message);
    }
}
