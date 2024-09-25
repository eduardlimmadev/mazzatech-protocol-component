using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Protocol.Shared.Domain.Dtos;
using Protocol.Shared.Domain.Validators;

namespace Protocol.Shared.Domain.IOC
{
    public static class DomainIOC
    {
        public static IServiceCollection AddDomainValidators(this IServiceCollection services)
            => services.AddScoped<IValidator<ProtocolDto>, ProtocolDtoValidator>();
    }
}
