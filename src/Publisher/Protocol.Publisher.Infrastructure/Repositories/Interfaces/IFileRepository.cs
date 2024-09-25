using Microsoft.AspNetCore.Http;

namespace Protocol.Publisher.Service.Services.Interfaces
{
    public interface IFileRepository
    {
        Task<Guid?> UploadFile(IFormFile file);
    }
}
