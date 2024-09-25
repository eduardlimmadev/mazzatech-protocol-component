using MongoDB.Bson;
using Protocol.FileService.Domain.Entities;

namespace Protocol.FileService.Application.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task AddAsync(FileEntity fileEntity);
        Task<FileEntity> GetByIdAsync(ObjectId id);
        Task<FileEntity> GetByFileIdAsync(Guid fileId);
    }
}
