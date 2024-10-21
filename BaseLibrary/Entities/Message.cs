namespace BaseLibrary.Entities
{
    /// <summary>
    /// Defines the <see cref="Message" />
    /// </summary>
    public class Message : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Text
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        // Отправитель сообщения

        /// <summary>
        /// Gets or sets the SenderId
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Gets or sets the Sender
        /// </summary>
        public User? Sender { get; set; }

        // Получатель сообщения

        /// <summary>
        /// Gets or sets the RecipientId
        /// </summary>
        public Guid RecipientId { get; set; }

        /// <summary>
        /// Gets or sets the Recipient
        /// </summary>
        public User? Recipient { get; set; }

        // Чат к которому относится сообщение

        /// <summary>
        /// Gets or sets the ChatId
        /// </summary>
        public Guid ChatId { get; set; }

        /// <summary>
        /// Gets or sets the Chat
        /// </summary>
        public Chat? Chat { get; set; }

        /// <summary>
        /// Gets or sets the Files
        /// </summary>
        public ICollection<Files>? Files { get; set; }
    }
}