using Mapster;
using Protocol.Domain.ValueObjects;
using Protocol.Shared.Domain.Dtos;

namespace Protocol.Application.IOC
{
    public static class MapsterIOC
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Domain.Entities.Protocol, ProtocolDto>
                .NewConfig()
                .Map(dest => dest.Cpf, src => src.Cpf.ToString())
                .Map(dest => dest.Rg, src => src.Rg.ToString());

            TypeAdapterConfig<ProtocolDto, Domain.Entities.Protocol>
                .NewConfig()
                .Map(dest => dest.ProtocolNumber, src => src.ProtocolNumber)
                .Map(dest => dest.ViaNumber, src => src.ViaNumber)
                .Map(dest => dest.Cpf, src => new Cpf(src.Cpf))
                .Map(dest => dest.Rg, src => new Rg(src.Rg))
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.MotherName, src => src.MotherName)
                .Map(dest => dest.FatherName, src => src.FatherName)
                .Map(dest => dest.PhotoId, src => src.PhotoId);
        }
    }
}
