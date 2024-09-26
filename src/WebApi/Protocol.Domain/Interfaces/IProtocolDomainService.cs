namespace Protocol.Domain.Interfaces
{
    public interface IProtocolDomainService
    {
        Task ValidateAndAddProtocolAsync(Entities.Protocol protocol);
    }
}
