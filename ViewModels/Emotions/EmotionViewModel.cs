using System.Collections.Generic;

namespace MyEmotionsApi.API.ViewModels
{
    public class EmotionViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string[] Tags { get; set; }
        public long PublishTime { get; set; }
        public string OwnerUsername { get; set; } 
        public string Content { get; set; } 
        public string OwnerId { get; set; }  
    }
}