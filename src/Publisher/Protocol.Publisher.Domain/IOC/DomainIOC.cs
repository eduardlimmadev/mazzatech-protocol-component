using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Protocol.Publisher.Domain.Options;

namespace Protocol.Publisher.Domain.IOC
{
    public static class DomainIOC
    {
        public static IServiceCollection AddCustomConfigurations(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMqOptions"))
                .Configure<Hosts>(configuration.GetSection("Hosts"));
    }
}
