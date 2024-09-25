using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Protocol.FileService.Application.Repositories.Interfaces;
using Protocol.FileService.Application.Services.Interfaces;
using Protocol.FileService.Infrastructure.Options;
using Protocol.FileService.Infrastructure.Repositories;
using Protocol.FileService.Infrastructure.Services;
using Serilog;

namespace Protocol.FileService.Infrastructure.IOC
{
    public static class InfrastructureIOC
    {
        public static IServiceCollection AddCustomConfigurations(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<FileStorageOptions>(configuration.GetSection("FileStorageOptions"))
                .Configure<MongoDBOptions>(configuration.GetSection("MongoDBOptions"));

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddSingleton<IFileRepository, FileRepository>();

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
            => services.AddSingleton<IFileStorageService, LocalFileStorageService>();

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbOptions = configuration.GetSection("MongoDBOptions").Get<MongoDBOptions>();

            var connectionString = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING")) ? Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING") : mongoDbOptions.ConnectionString;

            var dbName = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MONGODB_DATABASE")) ? Environment.GetEnvironmentVariable("MONGODB_DATABASE") : mongoDbOptions.Database;

            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(dbName))
                throw new Exception("Database connection is required");

            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(dbName);
            return services.AddSingleton(database);
        }

        public static IServiceCollection AddCustomLogger(this IServiceCollection services, IConfiguration configuration)
            => services.AddLogging(loggingBuilder =>
                loggingBuilder
                    .ClearProviders()
                    .AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger())
            );
    }
}
