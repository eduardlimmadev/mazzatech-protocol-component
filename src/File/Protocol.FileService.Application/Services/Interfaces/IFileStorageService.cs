using Microsoft.AspNetCore.Http;
using Protocol.FileService.Application.Dtos;
using Protocol.FileService.Domain.Entities;

namespace Protocol.FileService.Application.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<FileEntity> UploadFileAsync(IFormFile file);
        Task<FileDto> DownloadFileAsync(string fileName);
    }
}
