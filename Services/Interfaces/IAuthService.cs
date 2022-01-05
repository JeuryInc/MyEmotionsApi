using MyEmotionsApi.ViewModels;

namespace MyEmotionsApi.Services.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string actualPassword, string hashedPassword);
        AuthDataViewModel GetAuthData(string id);
    }
}