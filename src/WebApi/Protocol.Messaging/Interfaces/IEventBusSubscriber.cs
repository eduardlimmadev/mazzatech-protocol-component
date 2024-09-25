namespace Protocol.Messaging.Interfaces
{
    public interface IEventBusSubscriber
    {
        void Subscribe<T>(string topic, string queueGroup, Func<T, Task> handler);
    }
}
