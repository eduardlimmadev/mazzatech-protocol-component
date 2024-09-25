using Protocol.Consumer.Worker.Consumer.Interfaces;

namespace Protocol.Consumer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IRabbitMQConsumer _rabbitMqConsumer;
        private readonly ILogger<Worker> _logger;

        public Worker(IRabbitMQConsumer rabbitMqConsumer, ILogger<Worker> logger)
        {
            _rabbitMqConsumer = rabbitMqConsumer;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _rabbitMqConsumer.StartConsumer(stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Worker.ExecuteAsync - Error processing message");
            }
        }
    }
}
