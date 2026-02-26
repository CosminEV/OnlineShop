using System.ComponentModel.DataAnnotations;

namespace MagazineOnline.Api.ViewModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Parola")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Tine-ma minte")]
    public bool RememberMe { get; set; }
}
