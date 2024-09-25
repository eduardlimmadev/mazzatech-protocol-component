using Protocol.Publisher.Domain.Dtos;
using Protocol.Publisher.Shared.FlowControl.Models;

namespace Protocol.Publisher.Application.AppServices.Interfaces
{
    public interface IPublisherAppService
    {
        Task<SimpleResult> PublishMessageAsync(PublishProtocolDto protocol);
    }
}
