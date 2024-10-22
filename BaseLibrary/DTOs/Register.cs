namespace BaseLibrary.DTOs
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="Register" />
    /// </summary>
    public class Register : AccountBase
    {
        /// <summary>
        /// Gets or sets the FullName
        /// </summary>
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? FullName { get; set; }

        /// <summary>
        /// Gets or sets the ConfirmPassword
        /// </summary>
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Required]
        public string? ConfirmPassword { get; set; }
    }
}