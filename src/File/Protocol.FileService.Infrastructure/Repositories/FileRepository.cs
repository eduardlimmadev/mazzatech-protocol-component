using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Protocol.FileService.Application.Repositories.Interfaces;
using Protocol.FileService.Domain.Entities;
using System.Text.Json;

namespace Protocol.FileService.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IMongoCollection<FileEntity> _fileCollection;
        private readonly ILogger<FileRepository> _logger;

        public FileRepository(IMongoDatabase database, ILogger<FileRepository> logger)
        {
            _fileCollection = database.GetCollection<FileEntity>("files");
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            _logger = logger;
        }

        public async Task AddAsync(FileEntity fileEntity)
        {
            _logger.LogInformation("FileRepository.AddAsync - Prepare to insert file: {file}", JsonSerializer.Serialize(fileEntity));

            await _fileCollection.InsertOneAsync(fileEntity);
        }

        public async Task<FileEntity> GetByFileIdAsync(Guid fileId)
        {
            return await _fileCollection.Find(f => f.FileId.ToString().Equals(fileId.ToString())).FirstOrDefaultAsync();
        }

        public async Task<FileEntity> GetByIdAsync(ObjectId id)
        {
            return await _fileCollection.Find(f => f.Id == id).FirstOrDefaultAsync();
        }
    }
}
