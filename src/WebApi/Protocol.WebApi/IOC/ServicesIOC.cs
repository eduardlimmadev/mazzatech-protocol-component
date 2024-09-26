using Protocol.WebApi.Services;
using Protocol.WebApi.Services.Interfaces;

namespace Protocol.WebApi.IOC
{
    public static class ServicesIOC
    {
        public static IServiceCollection AddTokenService(this IServiceCollection services)
            => services.AddSingleton<ITokenService, TokenService>();
    }
}
