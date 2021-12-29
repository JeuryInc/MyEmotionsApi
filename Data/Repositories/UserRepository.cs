using MyEmotionsApi.Data;
using MyEmotionsApi.Data.Interfaces;
using MyEmotionsApi.Data.Repositories;
using MyEmotionsApi.Entities;

namespace Blog.Data.Repositories {
    public class UserRepository : BaseRepository<User>, IUserRepository {
        public UserRepository (MyEmotionDbContext context) : base (context) { }

        public bool IsValidUsername(string username)
        {
            var user = GetSingle(u => u.Username == username);
            return user == null;
        }
    }
}