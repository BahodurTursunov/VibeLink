using BaseLibrary.Enums;

namespace BaseLibrary.Entities
{
    public class Chat : BaseEntity
    {
        public string? Name { get; set; }
        public ChatType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<User>? Participants { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
