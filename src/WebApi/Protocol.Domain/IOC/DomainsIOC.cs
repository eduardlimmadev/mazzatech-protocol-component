using Microsoft.Extensions.DependencyInjection;
using Protocol.Domain.Interfaces;
using Protocol.Domain.Service;

namespace Protocol.Domain.IOC
{
    public static class DomainsIOC
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
            => services.AddScoped<IProtocolDomainService, ProtocolDomainService>();
    }
}
