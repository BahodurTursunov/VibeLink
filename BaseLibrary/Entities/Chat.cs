namespace BaseLibrary.Entities
{
    using BaseLibrary.Enums;

    /// <summary>
    /// Defines the <see cref="Chat" />
    /// </summary>
    public class Chat : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public ChatType Type { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the Participants
        /// </summary>
        public ICollection<User>? Participants { get; set; }

        /// <summary>
        /// Gets or sets the Messages
        /// </summary>
        public ICollection<Message>? Messages { get; set; }
    }
}