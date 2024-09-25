using Microsoft.AspNetCore.Http;

namespace Protocol.Publisher.Domain.Dtos
{
    public class PublishProtocolDto
    {
        public long ProtocolNumber { get; set; }
        public int ViaNumber { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Name { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public IFormFile Photo { get; set; }
        public string? PhotoPath { get; set; }
    }
}
