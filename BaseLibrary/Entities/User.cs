namespace BaseLibrary.Entities
{
    using BaseLibrary.Enums;

    /// <summary>
    /// Defines the <see cref="User" />
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the PasswordHash
        /// </summary>
        public string? PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the AvatarUrl
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the Chats
        /// </summary>
        public ICollection<Chat>? Chats { get; set; }

        /// <summary>
        /// Gets or sets the Messages
        /// </summary>
        public ICollection<Message>? Messages { get; set; }
    }
}