using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Protocol.Consumer.Domain.Options;

namespace Protocol.Consumer.Domain.IOC
{
    public static class DomainIOC
    {
        public static IServiceCollection AddCustomConfigurations(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMqOptions"))
                .Configure<Hosts>(configuration.GetSection("Hosts"));
    }
}
