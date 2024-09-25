using Microsoft.Extensions.DependencyInjection;
using Protocol.FileService.Application.Services;
using Protocol.FileService.Application.Services.Interfaces;

namespace Protocol.FileService.Application.IOC
{
    public static class AppServiceIOC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services.AddSingleton<IFileAppService, FileAppService>();
    }
}
