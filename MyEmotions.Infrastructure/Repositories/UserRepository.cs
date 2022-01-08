using MyEmotions.Core.Entities;
using MyEmotions.Core.Interfaces.Repositories;
using MyEmotions.Infrastructure.Data;

namespace MyEmotions.Infrastructure.Repositories {

    public class UserRepository : BaseRepository<User>, IUserRepository {
        public UserRepository (MyEmotionDbContext context) : base (context) { }

        public bool IsValidUsername(string username)
        {
            var user = GetSingle(u => u.Username == username);
            return user == null;
        }
    }
}