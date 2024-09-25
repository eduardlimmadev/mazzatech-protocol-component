namespace Protocol.Messaging.Interfaces
{
    public interface IMessageHandler<TMessage>
    {
        Task ProcessMessageAsync(TMessage message);
    }
}
