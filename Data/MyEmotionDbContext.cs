using Microsoft.EntityFrameworkCore;
using MyEmotionsApi.Entities;
using System;

namespace MyEmotionsApi.Data
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
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Password)
                .HasMaxLength(80)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Name)
                .HasMaxLength(60);
        }

        void ConfigureModelBuilderForEmotion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emotion>().ToTable("Emotion");
            modelBuilder.Entity<Emotion>()
                .Property(s => s.Title)
                .HasMaxLength(100);

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
