using System.ComponentModel.DataAnnotations;

namespace MagazineOnline.Api.ViewModels;

public class CreateProductViewModel
{
    [Required]
    [Display(Name = "Nume")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Descriere")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.1, 1_000_000)]
    [Display(Name = "Pret")]
    public decimal Price { get; set; }

    [Required]
    [Range(0, 100000)]
    [Display(Name = "Stoc")]
    public int Stock { get; set; }

    [Required]
    [Display(Name = "Categorie")]
    public string Category { get; set; } = "General";

    [Display(Name = "Produs recomandat")]
    public bool IsFeatured { get; set; }

    [Display(Name = "Imagine produs")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "Sau link imagine")]
    [Url]
    public string? ImageUrl { get; set; }
}
