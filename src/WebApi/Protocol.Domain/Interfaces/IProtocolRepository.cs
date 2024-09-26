namespace Protocol.Domain.Interfaces
{
    public interface IProtocolRepository
    {
        Task<bool> ExistsByProtocolNumberAsync(long protocolNumber);
        Task<bool> ExistsByRgOrCpfAndViaAsync(string rgOrCpf, int viaNumber);
        Task<Entities.Protocol> AddAsync(Entities.Protocol protocol);
        Task<Entities.Protocol?> GetProtocolByNumberAsync(long protocolNumber);
        Task<IEnumerable<Entities.Protocol>> GetAllProtocolsByRgAsync(string rg);
        Task<IEnumerable<Entities.Protocol>> GetAllProtocolsByCpfAsync(string cpf);
    }
}
