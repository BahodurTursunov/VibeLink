namespace BaseLibrary.Entities
{
    public class UserRole : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
