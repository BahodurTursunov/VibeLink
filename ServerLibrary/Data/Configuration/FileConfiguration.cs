using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServerLibrary.Data.Configuration
{
    public class FileConfiguration : IEntityTypeConfiguration<Files>
    {
        public void Configure(EntityTypeBuilder<Files> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder
                .Property(n => n.FileName)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder
                .Property(n => n.FileType)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder
                .Property(n => n.Url)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder
                .HasOne(m => m.Message)
                .WithMany()
                .HasForeignKey(m => m.MessageId);
        }
    }
}