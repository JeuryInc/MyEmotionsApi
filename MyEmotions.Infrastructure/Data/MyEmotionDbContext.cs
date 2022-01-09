using Microsoft.EntityFrameworkCore;
using MyEmotions.Core.Entities;
using MyEmotions.Infrastructure.Configure; 

namespace MyEmotions.Infrastructure.Data
{
    public class MyEmotionDbContext : DbContext
    {
        public MyEmotionDbContext(DbContextOptions<MyEmotionDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new EmotionConfiguration());
        } 
    }
}
