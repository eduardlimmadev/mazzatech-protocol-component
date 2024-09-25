using Microsoft.Extensions.Options;
using Protocol.Publisher.Infrastructure.Connections.Interfaces;
using Protocol.Publisher.Domain.Options;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;

namespace Protocol.Publisher.Infrastructure.Connections
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly RabbitMQOptions _rabbitMQConfig;
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMQConnection> _logger;
        private readonly IConnection _connection;
        private IModel _channel;

        public RabbitMQConnection(IOptions<RabbitMQOptions> rabbitMQOptions, ILogger<RabbitMQConnection> logger)
        {
            _rabbitMQConfig = rabbitMQOptions.Value ?? throw new ArgumentNullException(nameof(rabbitMQOptions));

            _logger = logger;

            var hostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? _rabbitMQConfig.Host;
            var port = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("RABBITMQ_PORT")) ? int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT")) : 5672;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = hostName,
                Port = port,
                Password = _rabbitMQConfig.Password,
                UserName = _rabbitMQConfig.Username
            };

            _connection = _connectionFactory.CreateConnection("Protocol-Publisher");
            _channel = GetChannel();
        }

        public void Close()
        {
            _connection.Close();
            _channel.Close();
        }

        public IModel GetChannel()
        {
            if (_channel != null)
                return _channel;

            _channel = _connection.CreateModel();

            _channel.BasicQos(0, 25, false);

            _channel.BasicAcks += (sender, eventArgs) =>
            {
                _logger.LogInformation("Message sent successfully");
            };

            _channel.BasicNacks += (sender, eventArgs) =>
            {
                _logger.LogError("Message was not sent ");
            };

            _channel.ConfirmSelect();
            return _channel;
        }

        public IConnection GetConnection()
            => _connection;
    }
}
