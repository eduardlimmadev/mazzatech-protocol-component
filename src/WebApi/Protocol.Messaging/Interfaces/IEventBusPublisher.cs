namespace Protocol.Messaging.Interfaces
{
    public interface IEventBusPublisher
    {
        void Publish<T>(T message, string routingKey);
    }
}
