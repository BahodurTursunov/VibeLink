namespace BaseLibrary.Entities
{
    /// <summary>
    /// Defines the <see cref="UserRole" />
    /// </summary>
    public class UserRole : BaseEntity
    {
        /// <summary>
        /// Gets or sets the RoleId
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public Guid UserId { get; set; }
    }
}
