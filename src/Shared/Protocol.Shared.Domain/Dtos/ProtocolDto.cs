
namespace Protocol.Shared.Domain.Dtos
{
    public class ProtocolDto
    {
        public ulong Id { get; set; }
        public long ProtocolNumber { get; set; }
        public int ViaNumber { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Name { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public Guid PhotoId { get; set; }
    }
}
