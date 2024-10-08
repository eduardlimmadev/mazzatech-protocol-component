﻿using FluentValidation;
using Mapster;
using Protocol.Application.Interfaces;
using Protocol.Domain.Interfaces;
using Protocol.Shared.Domain.Dtos;

namespace Protocol.Application.Services
{
    public class ProtocolAppService : IProtocolAppService
    {
        private readonly IProtocolRepository _repository;
        private readonly IProtocolDomainService _domainService;
        private readonly IValidator<ProtocolDto> _validator;

        public ProtocolAppService(IProtocolRepository repository, IProtocolDomainService domainService, IValidator<ProtocolDto> validator)
        {
            _repository = repository;
            _domainService = domainService;    
            _validator = validator;
        }

        public async Task<bool> CreateProtocolAsync(ProtocolDto protocolDto)
        {
            var protocol = protocolDto.Adapt<Domain.Entities.Protocol>();

            await _domainService.ValidateAndAddProtocolAsync(protocol);
            return true;
        }

        public async Task<ProtocolDto> GetProtocolByNumberAsync(long protocolNumber)
        {
            var protocol = await _repository.GetProtocolByNumberAsync(protocolNumber);
            if (protocol == null)
                return null;

            var protocolDto = protocol.Adapt<ProtocolDto>();
            return protocolDto;
        }

        public async Task<IEnumerable<ProtocolDto>> GetAllProtocolsByRgAsync(string rg)
        {
            var protocols = await _repository.GetAllProtocolsByRgAsync(rg);
            if (protocols == null || !protocols.Any())
                return Enumerable.Empty<ProtocolDto>();

            var protocolDtos = protocols.Adapt<IEnumerable<ProtocolDto>>();
            return protocolDtos;
        }

        public async Task<IEnumerable<ProtocolDto>> GetAllProtocolsByCpfAsync(string cpf)
        {
            var protocols = await _repository.GetAllProtocolsByRgAsync(cpf);
            if (protocols == null || !protocols.Any())
                return Enumerable.Empty<ProtocolDto>();

            var protocolDtos = protocols.Adapt<IEnumerable<ProtocolDto>>();
            return protocolDtos;
        }
    }
}
