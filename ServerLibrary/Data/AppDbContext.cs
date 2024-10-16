﻿using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Chat>()
                 .HasKey(pk => pk.Id);

            builder.Entity<Chat>()
                .Property(n => n.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Entity<Chat>()
                .HasMany(u => u.Participants)
                .WithOne();

            builder.Entity<Chat>()
                .HasMany(u => u.Messages)
                .WithOne();



            builder.Entity<User>()
                 .HasKey(pk => pk.Id);

            builder.Entity<User>()
                .Property(n => n.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Entity<User>()
                .Property(n => n.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");



            builder.Entity<Files>()
                .HasKey(pk => pk.Id);

            builder.Entity<Files>()
                .Property(n => n.FileName)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Entity<Files>()
                .Property(n => n.FileType)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Entity<Files>()
                .Property(n => n.Url)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Entity<Files>()
                .HasOne(m => m.Message)
                .WithMany()
                .HasForeignKey(m => m.MessageId);


            builder.Entity<Message>()
                .HasKey(pk => pk.Id);

            builder.Entity<Message>()
                .Property(t => t.Text)
                .IsRequired()
                .HasColumnType("varchar(2000)");

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany()
                .HasForeignKey(s => s.SenderId);
        }

        /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
              modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
          }*/
    }
}
