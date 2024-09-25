using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Protocol.Publisher.Infrastructure.Polly.Interfaces;
using Protocol.Publisher.Infrastructure.Polly;
using Protocol.Publisher.Infrastructure.Connections;
using Protocol.Publisher.Infrastructure.Connections.Interfaces;
using Protocol.Publisher.Infrastructure.Repositories;
using Protocol.Publisher.Infrastructure.Repositories.Interfaces;
using Protocol.Publisher.Service.Services.Interfaces;
using Serilog;

namespace Protocol.Publisher.Infrastructure.IOC
{
    public static class InfrastructureIOC
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddSingleton<IRabbitMQRepository, RabbitMQRepository>()
                .AddScoped<IFileRepository, FileRepository>();

        public static IServiceCollection AddInfrastructureConnections(this IServiceCollection services)
            => services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();

        public static IServiceCollection AddCustomLogger(this IServiceCollection services, IConfiguration configuration)
            => services.AddLogging(loggingBuilder =>
                loggingBuilder
                    .ClearProviders()
                    .AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger())
            );

        public static IServiceCollection AddPolly(this IServiceCollection services)
            => services.AddSingleton<IHttpClientPolicy, HttpClientPolicy>();
    }
}
