namespace Protocol.Consumer.Application.AppServices.Interfaces
{
    public interface IRabbitMQConsumerAppService
    {
        Task<bool> HandleMessage(string message);
    }
}
