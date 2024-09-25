using Protocol.Consumer.Service.Services.Interfaces;
using Protocol.Shared.Domain.Dtos;

namespace Protocol.Consumer.Service.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateProtocol(ProtocolDto? protocol)
        {
            if (protocol == null)
                return false;

            return true;
        }
    }
}
