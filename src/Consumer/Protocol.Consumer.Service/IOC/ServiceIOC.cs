using Microsoft.Extensions.DependencyInjection;
using Protocol.Consumer.Service.Services;
using Protocol.Consumer.Service.Services.Interfaces;

namespace Protocol.Consumer.Service.IOC
{
    public static class ServiceIOC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services.AddScoped<IValidationService, ValidationService>();
    }
}
