using Protocol.Domain.ValueObjects;

namespace Protocol.Domain.Entities
{
    public class Protocol
    {
        public ulong Id { get; private set; }
        public long ProtocolNumber { get; private set; }
        public int ViaNumber { get; private set; }
        public Cpf Cpf { get; private set; }
        public Rg Rg { get; private set; }
        public string Name { get; private set; }
        public string MotherName { get; private set; }
        public string FatherName { get; private set; }
        public Guid PhotoId { get; private set; }

        private Protocol() { }

        public Protocol(long protocolNumber, int viaNumber, Cpf cpf, Rg rg, string name, string motherName, string fatherName, Guid photoId)
        {
            ProtocolNumber = protocolNumber;
            ViaNumber = viaNumber;
            Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
            Rg = rg ?? throw new ArgumentNullException(nameof(rg));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MotherName = motherName;
            FatherName = fatherName;
            PhotoId = photoId;
        }
    }
}
