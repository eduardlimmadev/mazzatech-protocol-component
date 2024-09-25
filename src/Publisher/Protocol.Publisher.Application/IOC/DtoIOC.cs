using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Protocol.Publisher.Domain.Dtos;
using Protocol.Shared.Domain.Validators;

namespace Protocol.Publisher.Application.IOC
{
    public static class DtoIOC
    {
        public static IServiceCollection AddApplicationValidators(this IServiceCollection services)
            => services.AddScoped<IValidator<PublishProtocolDto>, PublishProtocolDtoValidator>();
    }
}
