using Microsoft.Extensions.DependencyInjection;
using Protocol.Publisher.Application.AppServices;
using Protocol.Publisher.Application.AppServices.Interfaces;

namespace Protocol.Publisher.Application.IOC
{
    public static class AppServiceIOC
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
            => services.AddScoped<IPublisherAppService, PublisherAppService>();
    }
}
