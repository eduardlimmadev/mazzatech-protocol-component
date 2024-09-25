using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Protocol.FileService.Domain.Entities
{
    public class FileEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public Guid FileId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
