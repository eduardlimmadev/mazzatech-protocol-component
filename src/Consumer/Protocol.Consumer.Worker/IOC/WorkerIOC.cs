using Protocol.Consumer.Worker.Consumer;
using Protocol.Consumer.Worker.Consumer.Interfaces;

namespace Protocol.Consumer.Worker.IOC
{
    public static class WorkerIOC
    {
        public static IServiceCollection AddConsumers(this IServiceCollection services)
            => services.AddScoped<IRabbitMQConsumer, RabbitMQConsumer>();
    }
}
