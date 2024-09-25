using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using Protocol.Shared.Domain.Dtos;

namespace Protocol.Shared.Domain.Builder.Dtos
{
    public class ProtocolDtoBuilder : Faker<ProtocolDto>
    {
        public ProtocolDtoBuilder() : base("pt_BR")
        {
            var faker = new Faker();
            var randomRgDigits = faker.Random.Int(9, 15);

            RuleFor(x => x.ProtocolNumber, (faker, dto) => faker.Random.Long(min: 1))
                .RuleFor(x => x.ViaNumber, (faker, dto) => faker.Random.Long(min: 1))
                .RuleFor(x => x.Cpf, (faker, dto) => faker.Person.Cpf(false))
                .RuleFor(x => x.Rg, (faker, dto) => faker.Random.Long(0, (long)Math.Pow(10, randomRgDigits) - 1).ToString($"D{randomRgDigits}"))
                .RuleFor(x => x.Name, (faker, dto) => faker.Person.FullName)
                .RuleFor(x => x.MotherName, (faker, dto) => faker.Random.Bool() ? faker.Name.FullName(Name.Gender.Female) : String.Empty)
                .RuleFor(x => x.FatherName, (faker, dto) => faker.Random.Bool() ? faker.Name.FullName(Name.Gender.Male) : String.Empty)
                .RuleFor(x => x.PhotoId, (faker, dto) => faker.Random.Guid());
        }

        public ProtocolDtoBuilder WithProtocolNumber(long protocolNumber)
        {
            RuleFor(x => x.ProtocolNumber, protocolNumber);
            return this;
        }

        public ProtocolDtoBuilder WithViaNumber(long viaNumber)
        {
            RuleFor(x => x.ViaNumber, viaNumber);
            return this;
        }

        public ProtocolDtoBuilder WithCpf(string cpf)
        {
            RuleFor(x => x.Cpf, cpf);
            return this;
        }

        public ProtocolDtoBuilder WithRg(string rg)
        {
            RuleFor(x => x.Rg, rg);
            return this;
        }

        public ProtocolDtoBuilder WithName(string name)
        {
            RuleFor(x => x.Name, name);
            return this;
        }

        public ProtocolDtoBuilder WithMotherName(string motherName)
        {
            RuleFor(x => x.MotherName, motherName);
            return this;
        }

        public ProtocolDtoBuilder WithFatherName(string fatherName)
        {
            RuleFor(x => x.FatherName, fatherName);
            return this;
        }

        public ProtocolDtoBuilder WithPhotoId(Guid photoId)
        {
            RuleFor(x => x.PhotoId, photoId);
            return this;
        }
    }
}
