using MyEmotions.Core.Entities;

namespace MyEmotions.Core.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool IsValidUsername(string username);
    }
}