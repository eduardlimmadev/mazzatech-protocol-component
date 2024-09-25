using RabbitMQ.Client;

namespace Protocol.Publisher.Infrastructure.Connections.Interfaces
{
    public interface IRabbitMQConnection
    {
        void Close();
        IConnection GetConnection();
        IModel GetChannel();
    }
}
