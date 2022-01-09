using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyEmotions.Core.Entities; 

namespace MyEmotions.Infrastructure.Configure
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.ToTable("User");

            modelBuilder
                .Property(user => user.Username)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder
                .Property(user => user.Password)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder
                .Property(user => user.Name)
                .HasMaxLength(65);

            modelBuilder.HasData(new User
            {
                Id = "9245fe4a-d402-451c-b9ed-9c1a04247482",
                Username = "0xJhon",
                Password = "AEzBJudAeZRtsUqecj+bPZtID6nLZO1L1hoMwcWNfQUU4YnolRQ3CTGacNb1GSgvqQ==",
                Name = "John Doe"
            },
            new User
            {
                Id = "5a7939fd-59de-44bd-a092-f5d8434584de",
                Username = "0xThanos",
                Password = "AEzBJudAeZRtsUqecj+bPZtID6nLZO1L1hoMwcWNfQUU4YnolRQ3CTGacNb1GSgvqQ==",
                Name = "Jim Starlin"
            });

        }
    }
}
