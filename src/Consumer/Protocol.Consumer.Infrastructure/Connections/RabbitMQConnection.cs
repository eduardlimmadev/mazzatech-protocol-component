using Microsoft.Extensions.Options;
using Protocol.Consumer.Infrastructure.Connections.Interfaces;
using Protocol.Consumer.Domain.Options;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;

namespace Protocol.Consumer.Infrastructure.Connections
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly RabbitMQOptions _rabbitMQConfig;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private IModel _channel;
        private readonly ILogger<RabbitMQConnection> _logger;

        public RabbitMQConnection(IOptions<RabbitMQOptions> rabbitMQOptions, ILogger<RabbitMQConnection> logger)
        {
            _logger = logger;
            _rabbitMQConfig = rabbitMQOptions.Value ?? throw new ArgumentNullException(nameof(rabbitMQOptions));

            var hostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? _rabbitMQConfig.Host;
            var port = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("RABBITMQ_PORT")) ? int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT")) : 5672;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = hostName,
                Port = port,
                Password = _rabbitMQConfig.Password,
                UserName = _rabbitMQConfig.Username
            };

            _connection = _connectionFactory.CreateConnection("Protocol-Consumer");
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

            _logger.LogInformation("RabbitMQConnection.GetChannel - Prepare for QueueDeclare | DeadLetterQueue: {DeadLetterQueue}", _rabbitMQConfig.DeadLetterQueue);

            _channel.QueueDeclare(queue: _rabbitMQConfig.DeadLetterQueue,
                durable: true,
                exclusive: true,
                autoDelete: false,
                arguments: null);

            var arguments = new Dictionary<string, object>()
            {
                { "x-dead-letter-exchange", "" },
                { "x-dead-letter-routing-key", _rabbitMQConfig.DeadLetterQueue }
            };

            _logger.LogInformation("RabbitMQConnection.GetChannel - Prepare for QueueDeclare | Queue: {Queue} | Arguments: {arguments}", _rabbitMQConfig.Queue, arguments);

            _channel.QueueDeclare(queue: _rabbitMQConfig.Queue,
                durable: true,
                exclusive: true,
                autoDelete: false,
                arguments: arguments);

            _channel.ExchangeDeclare(exchange: _rabbitMQConfig.Exchange,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(_rabbitMQConfig.Queue, _rabbitMQConfig.Exchange, "#");

            _channel.BasicQos(0, 25, false);
            return _channel;
        }

        public IConnection GetConnection()
            => _connection;
    }
}
