using Microsoft.Extensions.DependencyInjection;
using Protocol.Application.Interfaces;
using Protocol.Application.Services;

namespace Protocol.Application.IOC
{
    public static class AppServiceIOC
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services.AddScoped<IProtocolAppService, ProtocolAppService>();
    }
}
