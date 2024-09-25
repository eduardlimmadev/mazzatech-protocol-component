using Protocol.Domain.Exceptions;
using Protocol.Domain.Interfaces;

namespace Protocol.Domain.Service
{
    public class ProtocolDomainService
    {
        private readonly IProtocolRepository _protocolRepository;

        public ProtocolDomainService(IProtocolRepository protocolRepository)
        {
            _protocolRepository = protocolRepository;
        }

        public async Task ValidateAndAddProtocolAsync(Entities.Protocol protocol)
        {
            bool protocolExists = await _protocolRepository.ExistsByProtocolNumberAsync(protocol.ProtocolNumber);
            if (protocolExists)
                throw new ProtocolAlreadyExistsException($"Já existe um protocolo com o número: {protocol.ProtocolNumber}");

            bool viaExistsCpf = await _protocolRepository.ExistsByRgOrCpfAndViaAsync(protocol.Cpf.Number, protocol.ViaNumber);
            if (viaExistsCpf)
                throw new ProtocolAlreadyExistsException($"Já existe uma via {protocol.ViaNumber} para o CPF: {protocol.Cpf.Number}");

            bool viaExistsRg = await _protocolRepository.ExistsByRgOrCpfAndViaAsync(protocol.Rg.Number, protocol.ViaNumber);
            if (viaExistsRg)
                throw new ProtocolAlreadyExistsException($"Já existe uma via {protocol.ViaNumber} para o RG: {protocol.Rg.Number}");

            await _protocolRepository.AddAsync(protocol);
        }
    }
}
