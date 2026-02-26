using System.ComponentModel.DataAnnotations;

namespace MagazineOnline.Api.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Nume utilizator")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Parolele nu se potrivesc")]
    [Display(Name = "Confirma parola")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
