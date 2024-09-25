using Protocol.Consumer.Application.IOC;
using Protocol.Consumer.Domain.IOC;
using Protocol.Consumer.Infrastructure.IOC;
using Protocol.Consumer.Service.IOC;
using Protocol.Consumer.Worker;
using Protocol.Consumer.Worker.Helper;
using Protocol.Consumer.Worker.IOC;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
    })
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        configuration.PrintConfiguration();

        services
            .AddConsumers()
            .AddCustomConfigurations(hostContext.Configuration)
            .AddHostedService<Worker>()
            .AddServices()
            .AddAppService()
            .AddRepositories()
            .AddInfraestructureConnections()
            .AddCustomLogger(configuration)
            .AddPolly()
            .AddHttpClient("ProtocolClient");
    })
    .Build();

await host.RunAsync();
