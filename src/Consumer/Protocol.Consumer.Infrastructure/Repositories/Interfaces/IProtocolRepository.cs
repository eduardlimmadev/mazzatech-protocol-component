using Protocol.Shared.Domain.Dtos;

namespace Protocol.Consumer.Infrastructure.Repositories.Interfaces
{
    public interface IProtocolRepository
    {
        Task<bool> SendProtocol(ProtocolDto protocol);
    }
}
