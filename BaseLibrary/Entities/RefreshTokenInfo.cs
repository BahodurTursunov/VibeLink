namespace BaseLibrary.Entities
{
    /// <summary>
    /// Defines the <see cref="RefreshTokenInfo" />
    /// </summary>
    public class RefreshTokenInfo : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Token
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public Guid UserId { get; set; }
    }
}