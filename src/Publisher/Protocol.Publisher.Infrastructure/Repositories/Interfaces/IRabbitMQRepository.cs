using Protocol.Publisher.Shared.FlowControl.Models;
using Protocol.Shared.Domain.Dtos;

namespace Protocol.Publisher.Infrastructure.Repositories.Interfaces
{
    public interface IRabbitMQRepository
    {
        SimpleResult PublishMessage(ProtocolDto message);
    }
}
