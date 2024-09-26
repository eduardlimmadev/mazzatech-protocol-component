using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Protocol.Domain.Interfaces;
using Protocol.Infrastructure.Data;
using Protocol.Infrastructure.Repositories;
using Serilog;

namespace Protocol.Infrastructure.IOC
{
    public static class InfrastructureIOC
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddScoped<IProtocolRepository, ProtocolRepository>();

        public static IServiceCollection AddCustomLogger(this IServiceCollection services, IConfiguration configuration)
            => services.AddLogging(loggingBuilder =>
                loggingBuilder.ClearProviders()
                    .AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger()));

        public static IServiceCollection AddDbContextConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ProtocolDbContext>(options =>
                options.UseNpgsql(connectionString)
            );
            return services;
        }
    }
}
