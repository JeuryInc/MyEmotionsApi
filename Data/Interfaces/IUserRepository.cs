using MyEmotionsApi.Entities;

namespace MyEmotionsApi.Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool IsValidUsername(string username);
    }
}