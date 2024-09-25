using Microsoft.Extensions.DependencyInjection;
using Protocol.Consumer.Application.AppServices;
using Protocol.Consumer.Application.AppServices.Interfaces;

namespace Protocol.Consumer.Application.IOC
{
    public static class AppServiceIOC
    {
        public static IServiceCollection AddAppService(this IServiceCollection services)
            => services.AddScoped<IRabbitMQConsumerAppService, RabbitMQConsumerAppService>();
    }
}
