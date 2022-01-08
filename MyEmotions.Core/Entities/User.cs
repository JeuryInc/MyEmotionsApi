using System.Collections.Generic;

namespace MyEmotions.Core.Entities
{
    public class User
    {
        public User()
        {
            Emotions = new List<Emotion>();
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public List<Emotion> Emotions { get; set; }
    }
}