using Microsoft.EntityFrameworkCore;
using MyEmotions.Core.Entities;
using System;

namespace MyEmotions.Infrastructure.Data
{
    public class MyEmotionDbContext : DbContext
    {
        public MyEmotionDbContext(DbContextOptions<MyEmotionDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureModelBuilderForUser(modelBuilder);
            ConfigureModelBuilderForEmotion(modelBuilder); 
        }

        void ConfigureModelBuilderForUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>()
                .Property(user => user.Username)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Password)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Name)
                .HasMaxLength(65);
        }

        void ConfigureModelBuilderForEmotion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emotion>().ToTable("Emotion");
            modelBuilder.Entity<Emotion>()
                .Property(s => s.Title)
                .HasMaxLength(180);

            modelBuilder.Entity<Emotion>()
                .Property(s => s.OwnerId)
                .IsRequired();

            modelBuilder.Entity<Emotion>()
                .HasOne(s => s.Owner)
                .WithMany(u => u.Emotions)
                .HasForeignKey(s => s.OwnerId);

            modelBuilder.Entity<Emotion>()
            .Property(e => e.Tags)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
