using System; 

namespace MyEmotionsApi.Entities
{
    public class Emotion
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsPublic { get; set; }

        public User Owner { get; set; }
        public string OwnerId { get; set; }
    }
}