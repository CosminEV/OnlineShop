using MagazineOnline.Api.Data;
using MagazineOnline.Api.Models;
using MagazineOnline.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagazineOnline.Api.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IWebHostEnvironment _environment;

    public ProductsController(AppDbContext dbContext, IWebHostEnvironment environment)
    {
        _dbContext = dbContext;
        _environment = environment;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var imageUrl = model.ImageUrl?.Trim();

        if (model.ImageFile is not null && model.ImageFile.Length > 0)
        {
            var uploadsDirectory = Path.Combine(_environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsDirectory);

            var extension = Path.GetExtension(model.ImageFile.FileName);
            var imageName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsDirectory, imageName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await model.ImageFile.CopyToAsync(stream);

            imageUrl = $"/uploads/{imageName}";
        }

        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            ModelState.AddModelError(nameof(model.ImageFile), "Adauga o poza prin upload sau link URL.");
            return View(model);
        }

        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Stock = model.Stock,
            ImageUrl = imageUrl
        };

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        TempData["Success"] = "Produsul a fost adaugat cu succes.";
        return RedirectToAction("Index", "Home");
    }
}
