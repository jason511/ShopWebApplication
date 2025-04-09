using System.ComponentModel.DataAnnotations;

namespace ShopWebApplication.Models;

public class AdminRegisterViewModel
{
        [Required]
        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "密碼與確認密碼不一致")]
        public string ConfirmPassword { get; set; }
}