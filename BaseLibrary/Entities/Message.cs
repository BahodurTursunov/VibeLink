namespace BaseLibrary.Entities
{
    public class Message : BaseEntity
    {
        public string? Text { get; set; }
        public DateTime Timestamp { get; set; }

        // Отправитель сообщения
        public Guid SenderId { get; set; }
        public User? Sender { get; set; }

        // Получатель сообщения
        public Guid RecipientId { get; set; }
        public User? Recipient { get; set; }

        // Чат к которому относится сообщение
        public Guid ChatId { get; set; }
        public Chat? Chat { get; set; }

        public ICollection<Files>? Files { get; set; }
    }
}
