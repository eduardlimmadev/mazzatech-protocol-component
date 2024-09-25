using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Protocol.Consumer.Infrastructure.Connections;
using Protocol.Consumer.Infrastructure.Connections.Interfaces;
using Protocol.Consumer.Infrastructure.Polly;
using Protocol.Consumer.Infrastructure.Polly.Interfaces;
using Protocol.Consumer.Infrastructure.Repositories;
using Protocol.Consumer.Infrastructure.Repositories.Interfaces;
using Serilog;

namespace Protocol.Consumer.Infrastructure.IOC
{
    public static class InfrastructureIOC
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddScoped<IProtocolRepository, ProtocolRepository>();

        public static IServiceCollection AddInfraestructureConnections(this IServiceCollection services)
            => services.AddScoped<IRabbitMQConnection, RabbitMQConnection>();

        public static IServiceCollection AddCustomLogger(this IServiceCollection services, IConfiguration configuration)
            => services.AddLogging(loggingBuilder => 
                loggingBuilder.ClearProviders()
                    .AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger()));

        public static IServiceCollection AddPolly(this IServiceCollection services)
            => services.AddSingleton<IHttpClientPolicy, HttpClientPolicy>();
    }
}
