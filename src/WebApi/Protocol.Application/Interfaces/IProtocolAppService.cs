using Protocol.Shared.Domain.Dtos;

namespace Protocol.Application.Interfaces
{
    public interface IProtocolAppService
    {
        Task<bool> CreateProtocolAsync(ProtocolDto protocol);
        Task<ProtocolDto> GetProtocolByNumberAsync(long protocolNumber);
        Task<IEnumerable<ProtocolDto>> GetAllProtocolsByRgAsync(string rg);
        Task<IEnumerable<ProtocolDto>> GetAllProtocolsByCpfAsync(string cpf);
    }
}
