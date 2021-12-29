using MyEmotionsApi.Data.Interfaces;
using MyEmotionsApi.Entities;

namespace MyEmotionsApi.Data.Repositories
{
    public class EmotionRepository : BaseRepository<Emotion>, IEmotionRepository 
    {
        public EmotionRepository(MyEmotionDbContext context) : base (context) { }

        public bool IsOwner(string emotionId, string userId)
        {
            var story = GetSingle(s => s.Id == emotionId);
            return story.OwnerId == userId;
        }
  }
}