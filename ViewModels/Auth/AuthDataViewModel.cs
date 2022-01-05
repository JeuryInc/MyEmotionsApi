namespace MyEmotionsApi.ViewModels
{
    public class AuthDataViewModel
    {
        public string Token { get; set; }
        public long TokenExpirationTime { get; set; }
        public string Id { get; set; }
    }
}