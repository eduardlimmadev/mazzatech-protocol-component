using Microsoft.Extensions.DependencyInjection;
using Protocol.Publisher.Service.Services;
using Protocol.Publisher.Service.Services.Interfaces;

namespace Protocol.Publisher.Service.IOC
{
    public static class ServiceIOC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services.AddScoped<IValidationService, ValidationService>();
    }
}
