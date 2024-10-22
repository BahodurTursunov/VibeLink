namespace BaseLibrary.Entities
{
    /// <summary>
    /// Defines the <see cref="Files" />
    /// </summary>
    public class Files : BaseEntity
    {
        /// <summary>
        /// Gets or sets the FileName
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the FileType
        /// </summary>
        public string? FileType { get; set; }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the MessageId
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public Message? Message { get; set; }
    }
}