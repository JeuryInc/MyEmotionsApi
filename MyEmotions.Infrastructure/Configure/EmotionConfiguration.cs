using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyEmotions.Core.Entities;
using System;

namespace MyEmotions.Infrastructure.Configure
{
    public class EmotionConfiguration : IEntityTypeConfiguration<Emotion>
    {
        public void Configure(EntityTypeBuilder<Emotion> modelBuilder)
        {
            modelBuilder.ToTable("Emotion");

            modelBuilder
                .Property(s => s.Title)
                .HasMaxLength(180);

            modelBuilder
                .Property(s => s.OwnerId)
                .IsRequired();

            modelBuilder
                .HasOne(s => s.Owner)
                .WithMany(u => u.Emotions)
                .HasForeignKey(s => s.OwnerId);

            modelBuilder
            .Property(e => e.Tags)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.HasData(
                new Emotion
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Oh man, today was a long work day.",
                    Content = @"I worked 12pm – 12am today and that was kind of super long. I didn’t have enough help for half of the day so I was stressed as fuck. I did have a great team though for the second half of the shift, so I was able to get my work done. I’ve been eating super bad and only once a day. I have to fix that somehow because my stomach hurts all day every day lately.  
                    I got a package in the mail today from anonymous…it was a coffee press!!!!! This morning Dave sent me a text that said “can you please get us a coffee press”,
                    I was already at work so I said “of course” and when he got home he sent me a picture of the coffee press and said “wow that was fast”!I don’t know who got this gift for us but we are both super super grateful! THANK YOU!!!",
                    Tags = new string[3] { "Long day", "Stress", "Surprise" },
                    CreationTime = DateTime.UtcNow,
                    IsPublic = true,
                    OwnerId = "9245fe4a-d402-451c-b9ed-9c1a04247482"
                }, 
                new Emotion
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "How I find time for reading?",
                    Content = @"I am a reader first and then I am a new person to ‘online’ diary writing but do write a ton in my journals I have at home. My journey with words began not when I was born and cried the hell out but when I picked my first novel. It was a Hindi novel titled Mayapuri by Shivani. Shivani is no more but her books continue to touch hearts. My first English novel was Five Point Someone by India’s one of the eminent writers, Chetan Bhagat. I was besotted when I had first read it.
                    The reader has travelled a long way since then, has met amazing people on papers and travelled seas and mountains.
                    Now, reading is an inseparable part of my routine. There used to be days when I am too deep into my writing a mind map (in my little hand journal) to pick a book. Or, simply busy doing something else. Reading then would take a back seat.
                    You guessed it right. The reader inside me hated it! The writer within scoffed and scorched with insults and contempt. I had to solve this problem.",
                    Tags = new string[2] { "Thoughtful", "Stress" },
                    CreationTime = DateTime.UtcNow,
                    IsPublic = true,
                    OwnerId = "9245fe4a-d402-451c-b9ed-9c1a04247482"
                },
                new Emotion
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "I want a new life",
                    Content = @"I wanted to eat healthy and get in shape so I am trying out low-impact cardio workouts. Dk when we will buy treadmill so just burning some calories now. Also training my heart, to have lower beat level. 152 was the max today but I didn't feel any pain nor I hyperventilated. 
                    Tried about 7 mins today while playing lo$er l♡ver of txt cause why not? It was fun.
                    Green tea is good for metabolism so I am cutting out coffee to just one cup and trying out green tea again. ",
                    Tags = new string[2] { "Thoughtful", "Love" },
                    CreationTime = DateTime.UtcNow,
                    IsPublic = true,
                    OwnerId = "5a7939fd-59de-44bd-a092-f5d8434584de"
                });
        }
    }
}
