using BaseLibrary.Enums;

namespace BaseLibrary.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? AvatarUrl { get; set; }
        public UserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Chat>? Chats { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
