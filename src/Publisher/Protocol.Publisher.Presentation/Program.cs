using Protocol.Publisher.Application.IOC;
using Protocol.Publisher.Domain.IOC;
using Protocol.Publisher.Infrastructure.IOC;
using Protocol.Publisher.Service.IOC;
using Protocol.Shared.Domain.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ProtocolsCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
}).ConfigureServices((hostingContext, services) =>
{
    var config = hostingContext.Configuration;
    services
        .AddCustomConfigurations(config)
        .AddAppServices()
        .AddServices()
        .AddRepositories()
        .AddInfrastructureConnections()
        .AddCustomLogger(config)
        .AddApplicationValidators()
        .AddDomainValidators()
        .AddPolly()
        .AddHttpClient("FileServiceClient");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
