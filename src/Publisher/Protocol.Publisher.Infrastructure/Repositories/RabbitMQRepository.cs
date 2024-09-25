using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Protocol.Publisher.Infrastructure.Repositories.Interfaces;
using Protocol.Publisher.Domain.Options;
using Protocol.Publisher.Shared.FlowControl.Enums;
using Protocol.Publisher.Shared.FlowControl.Models;
using Protocol.Shared.Domain.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Protocol.Publisher.Infrastructure.Connections.Interfaces;

namespace Protocol.Publisher.Infrastructure.Repositories
{
    public class RabbitMQRepository : IRabbitMQRepository
    {
        private readonly RabbitMQOptions _rabbitMQOptions;
        private readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly ILogger<RabbitMQRepository> _logger;

        public RabbitMQRepository(IOptions<RabbitMQOptions> rabbitMQOptions, IRabbitMQConnection rabbitMQConnection, ILogger<RabbitMQRepository> logger)
        {
            _rabbitMQOptions = rabbitMQOptions.Value ?? throw new ArgumentNullException(nameof(rabbitMQOptions));
            _channel = rabbitMQConnection.GetChannel();
            _exchangeName = _rabbitMQOptions.Exchange;
            _logger = logger;
        }

        public SimpleResult PublishMessage(ProtocolDto message)
        {
            try
            {
                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;

                var body = JsonSerializer.Serialize(message);

                _channel.BasicPublish(exchange: _exchangeName,
                    routingKey: "#",
                    basicProperties: properties,
                    body: Encoding.UTF8.GetBytes(body));

                _channel.WaitForConfirmsOrDie();
                return SimpleResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on message publishing");

                return SimpleResult.Fail(new Error(ErrorType.Unhandled, "PublishMessageError", "Error on message publishing"));
            }
        }
    }
}
