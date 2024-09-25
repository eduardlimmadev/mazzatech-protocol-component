using Microsoft.AspNetCore.Http;
using Protocol.FileService.Application.Dtos;

namespace Protocol.FileService.Application.Services.Interfaces
{
    public interface IFileAppService
    {
        Task<Guid> UploadFileAsync(IFormFile file);
        Task<FileDto> DownloadFileAsync(Guid fileId);
    }
}
