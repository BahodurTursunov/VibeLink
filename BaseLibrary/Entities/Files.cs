namespace BaseLibrary.Entities
{
    public class Files : BaseEntity
    {
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public string? Url { get; set; }
        public Guid MessageId { get; set; }
        public Message? Message { get; set; }
    }
}
