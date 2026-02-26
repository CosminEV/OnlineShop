using MagazineOnline.Api.Models;

namespace MagazineOnline.Api.ViewModels;

public class HomeIndexViewModel
{
    public IReadOnlyList<Product> Products { get; init; } = [];
    public IReadOnlyList<Product> FeaturedProducts { get; init; } = [];
    public IReadOnlyList<string> Categories { get; init; } = [];
    public string? SearchTerm { get; init; }
    public string? Category { get; init; }
    public string? Sort { get; init; }
}
