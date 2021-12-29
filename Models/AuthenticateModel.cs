using System.ComponentModel.DataAnnotations;

namespace MyEmotionsApi.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
