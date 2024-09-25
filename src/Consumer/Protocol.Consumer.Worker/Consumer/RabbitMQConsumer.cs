using Microsoft.Extensions.Options;
using Protocol.Consumer.Application.AppServices.Interfaces;
using Protocol.Consumer.Domain.Options;
using Protocol.Consumer.Infrastructure.Connections.Interfaces;
using Protocol.Consumer.Worker.Consumer.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Protocol.Consumer.Worker.Consumer
{
    public class RabbitMQConsumer : EventingBasicConsumer, IRabbitMQConsumer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMQConsumer> _logger;
        private readonly RabbitMQOptions _rabbitMQConfigs;

        public RabbitMQConsumer(ILogger<RabbitMQConsumer> logger, IServiceProvider serviceProvider, IRabbitMQConnection rabbitMQConnection, IOptions<RabbitMQOptions> rabbitMQOptions) : base(rabbitMQConnection.GetChannel())
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _rabbitMQConfigs = rabbitMQOptions?.Value ?? throw new ArgumentNullException(nameof(rabbitMQOptions));
        }

        public void onReceived(CancellationToken token)
        {
            this.Received += async (sender, e) =>
            {
                try
                {
                    if (token.IsCancellationRequested)
                        return;

                    string content = Encoding.UTF8.GetString(e.Body.ToArray());
                    using var scope = _serviceProvider.CreateScope();
                    var processAppService = scope.ServiceProvider.GetRequiredService<IRabbitMQConsumerAppService>();

                    var successfullHandled = await processAppService.HandleMessage(content);

                    if (successfullHandled)
                        Model.BasicAck(e.DeliveryTag, false);
                    else
                        Model.BasicNack(e.DeliveryTag, false, false);

                    if (!successfullHandled)
                        _logger.LogInformation("RabbitMQConsumer.OnReceived - Message sent: {successfullHandled} | Message consumed: {consumer}", successfullHandled, content);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "RabbitMQConsumer.OnReceived Error");
                }
            };
        }

        public void StartConsumer(CancellationToken token)
        {
            onReceived(token);
            this.Model.BasicConsume(
                queue: _rabbitMQConfigs.Queue,
                autoAck: false,
                consumer: this);
        }
    }
}
