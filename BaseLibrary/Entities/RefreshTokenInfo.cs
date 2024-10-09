namespace BaseLibrary.Entities
{
    public class RefreshTokenInfo : BaseEntity
    {
        public string? Token { get; set; }
        public Guid UserId { get; set; }
    }
}
