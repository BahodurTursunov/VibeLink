using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServerLibrary.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder.Property(n => n.Name).IsRequired().HasColumnType("varchar(100)");
            builder.Property(n => n.Email).IsRequired().HasColumnType("varchar(100)");
        }
    }
}
