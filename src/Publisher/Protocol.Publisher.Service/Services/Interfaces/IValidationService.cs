using Protocol.Publisher.Domain.Dtos;
using Protocol.Publisher.Shared.FlowControl.Models;
using Protocol.Shared.Domain.Dtos;

namespace Protocol.Publisher.Service.Services.Interfaces
{
    public interface IValidationService
    {
        Task<SimpleResult> ValidateAsync(ProtocolDto protocol);
        Task<SimpleResult> ValidateAsync(PublishProtocolDto protocol);
    }
}
