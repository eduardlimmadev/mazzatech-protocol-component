using RabbitMQ.Client;

namespace Protocol.Consumer.Infrastructure.Connections.Interfaces
{
    public interface IRabbitMQConnection
    {
        void Close();
        IConnection GetConnection();
        IModel GetChannel();
    }
}
