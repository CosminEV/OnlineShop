using System.ComponentModel.DataAnnotations;

namespace MagazineOnline.Api.Models;

public class Product
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(1500)]
    public string Description { get; set; } = string.Empty;

    [Range(0.1, 1_000_000)]
    public decimal Price { get; set; }

    [Range(0, 100_000)]
    public int Stock { get; set; }

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string Category { get; set; } = "General";

    public bool IsFeatured { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
