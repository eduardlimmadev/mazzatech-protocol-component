namespace Protocol.Messaging.Messages
{
    public record FileMessage
    {
        public byte[] FileContent { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
    }
}
