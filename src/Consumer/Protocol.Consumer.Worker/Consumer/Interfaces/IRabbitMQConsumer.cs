namespace Protocol.Consumer.Worker.Consumer.Interfaces
{
    public interface IRabbitMQConsumer
    {
        void onReceived(CancellationToken token);
        void StartConsumer(CancellationToken token);
    }
}
