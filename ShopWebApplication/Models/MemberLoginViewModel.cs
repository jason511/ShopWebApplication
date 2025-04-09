using System.ComponentModel.DataAnnotations;

namespace ShopWebApplication.Models;

public class MemberLoginViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}