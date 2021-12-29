using MyEmotionsApi.Entities;

namespace MyEmotionsApi.Data.Interfaces
{
    public interface IEmotionRepository: IBaseRepository<Emotion>
    {
        bool IsOwner(string emotionId, string userId);
    }
}