using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServerLibrary.Data.Configuration
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder
                .Property(n => n.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasMany(u => u.Participants)
                .WithOne();

            builder
                .HasMany(u => u.Messages)
                .WithOne();
        }
    }
}