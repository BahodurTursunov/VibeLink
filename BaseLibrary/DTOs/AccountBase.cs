namespace BaseLibrary.DTOs
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="AccountBase" />
    /// </summary>
    public class AccountBase
    {
        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        [Required]
        public string? Password { get; set; }
    }
}