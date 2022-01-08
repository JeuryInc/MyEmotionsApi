using System.ComponentModel.DataAnnotations;

namespace MyEmotionsApi.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 8)]
        public string Password { get; set; }
    }
}