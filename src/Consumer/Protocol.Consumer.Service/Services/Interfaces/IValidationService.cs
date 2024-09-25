using Protocol.Shared.Domain.Dtos;

namespace Protocol.Consumer.Service.Services.Interfaces
{
    public interface IValidationService
    {
        bool ValidateProtocol(ProtocolDto? protocol);
    }
}
