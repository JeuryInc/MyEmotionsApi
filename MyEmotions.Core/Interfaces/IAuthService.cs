using MyEmotions.Core.Model;

namespace MyEmotions.Core.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string actualPassword, string hashedPassword);
        AuthData GetAuthData(string id);
    }
}