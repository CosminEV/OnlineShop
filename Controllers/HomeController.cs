using MagazineOnline.Api.Data;
using MagazineOnline.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagazineOnline.Api.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _dbContext;

    public HomeController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index(string? q, string? category, string? sort)
    {
        var query = _dbContext.Products.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(p => p.Name.Contains(q) || p.Description.Contains(q));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(p => p.Category == category);
        }

        query = sort switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "name" => query.OrderBy(p => p.Name),
            _ => query.OrderByDescending(p => p.CreatedAtUtc)
        };

        var products = await query.ToListAsync();

        var featuredProducts = await _dbContext.Products
            .AsNoTracking()
            .Where(p => p.IsFeatured)
            .OrderByDescending(p => p.CreatedAtUtc)
            .Take(3)
            .ToListAsync();

        var categories = await _dbContext.Products
            .AsNoTracking()
            .Select(p => p.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        var model = new HomeIndexViewModel
        {
            Products = products,
            FeaturedProducts = featuredProducts,
            Categories = categories,
            SearchTerm = q,
            Category = category,
            Sort = sort
        };

        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }
}
