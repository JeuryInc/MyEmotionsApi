using MyEmotions.Core.Entities;

namespace MyEmotions.Core.Interfaces.Repositories
{
    public interface IEmotionRepository: IBaseRepository<Emotion>
    {
        bool IsOwner(string emotionId, string userId);
    }
}