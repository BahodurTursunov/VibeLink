namespace BaseLibrary.DTOs
{
    /// <summary>
    /// Defines the <see cref="UserSession" />
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// Gets or sets the Token
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the RefreshToken
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}