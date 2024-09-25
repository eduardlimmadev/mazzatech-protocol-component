using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Protocol.FileService.Application.Dtos;
using Protocol.FileService.Application.Services.Interfaces;
using Protocol.FileService.Domain.Entities;
using Protocol.FileService.Infrastructure.Options;

namespace Protocol.FileService.Infrastructure.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly FileStorageOptions _fileStorageOptions;
        private readonly ILogger<LocalFileStorageService> _logger;

        public LocalFileStorageService(IOptions<FileStorageOptions> fileStorageOptions, ILogger<LocalFileStorageService> logger)
        {
            _fileStorageOptions = GetFileStorageOptions(fileStorageOptions);
            _logger = logger;
        }

        private FileStorageOptions GetFileStorageOptions(IOptions<FileStorageOptions> fileStorageOptions)
        {
            var basePath = Environment.GetEnvironmentVariable("FILE_STORAGE_BASE_PATH");

            if (!string.IsNullOrEmpty(basePath))
                return new FileStorageOptions { BasePath = basePath };

            return fileStorageOptions?.Value ?? throw new ArgumentNullException(nameof(fileStorageOptions));
        }

        public async Task<FileEntity> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogError("FileStorageService.SaveFileAsync Error - The file cannot be null or empty");
                throw new ArgumentException("The file cannot be null or empty");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                _logger.LogError("FileStorageService.SaveFileAsync Error - File must be a JPG or PNG image\n File: {filename} | ContentType: {contenttype}", file.FileName, file.ContentType);
                throw new Exception("FileStorageService.SaveFileAsync Error - File must be a JPG or PNG image");
            }

            var guid = Guid.NewGuid();
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{guid}{extension}";
            var filePath = Path.Combine(_fileStorageOptions.BasePath, fileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return new FileEntity
                {
                    Id = ObjectId.GenerateNewId(),
                    FileId = guid,
                    Name = fileName,
                    Extension = extension,
                    MimeType = file.ContentType,
                    Path = filePath,
                    CreatedAt = DateTime.UtcNow
                };
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "FileStorageService.SaveFileAsync Error - An error occurred while trying to save the file");
                throw new InvalidOperationException("An error occurred while trying to save the file", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FileStorageService.SaveFileAsync Error - An unexpected error occurred while trying to save the file");
                throw new InvalidOperationException("An unexpected error occurred while trying to save the file", ex);
            }
        }

        public async Task<FileDto> DownloadFileAsync(string fileName)
        {
            var filePath = Path.Combine(_fileStorageOptions.BasePath, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.");

            var fileBytes = await File.ReadAllBytesAsync(filePath);
            var base64 = Convert.ToBase64String(fileBytes);
            var mimeType = GetMimeType(Path.GetExtension(fileName));

            return new FileDto
            {
                Base64 = base64,
                MimeType = mimeType,
                Extension = Path.GetExtension(fileName)
            };
        }

        private string GetMimeType(string extension)
        {
            return extension switch
            {
                ".jpg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
        }
    }
}
