using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Protocol.FileService.Application.Dtos;
using Protocol.FileService.Application.Repositories.Interfaces;
using Protocol.FileService.Application.Services.Interfaces;

namespace Protocol.FileService.Application.Services
{
    public class FileAppService : IFileAppService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<FileAppService> _logger;

        public FileAppService(IFileRepository fileRepository, IFileStorageService fileStorageService, ILogger<FileAppService> logger)
        {
            _fileRepository = fileRepository;
            _fileStorageService = fileStorageService;
            _logger = logger;
        }

        public async Task<FileDto> DownloadFileAsync(Guid fileId)
        {
            var fileEntity = await _fileRepository.GetByFileIdAsync(fileId);
            if (fileEntity == null)
            {
                _logger.LogError("File not found - FileId: {fileId}", fileId);
                throw new FileNotFoundException("File not found");
            }

            var fileBytes = await File.ReadAllBytesAsync(fileEntity.Path);
            var base64 = Convert.ToBase64String(fileBytes);

            return new FileDto
            {
                Base64 = base64,
                MimeType = fileEntity.MimeType,
                Extension = fileEntity.Extension
            };
        }

        public async Task<Guid> UploadFileAsync(IFormFile file)
        {
            var uploadedFile = await _fileStorageService.UploadFileAsync(file);

            await _fileRepository.AddAsync(uploadedFile);

            return uploadedFile.FileId;
        }
    }
}
