using MagazineOnline.Api.Data;
using MagazineOnline.Api.Extensions;
using MagazineOnline.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagazineOnline.Api.Controllers;

public class CartController : Controller
{
    private const string CartSessionKey = "cart_items";
    private readonly AppDbContext _dbContext;

    public CartController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var items = HttpContext.Session.GetJson<List<CartItemViewModel>>(CartSessionKey) ?? [];
        ViewBag.GrandTotal = items.Sum(x => x.Total);
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int productId, int quantity = 1)
    {
        if (quantity < 1)
        {
            quantity = 1;
        }

        var product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId);
        if (product is null)
        {
            TempData["Error"] = "Produsul nu a fost gasit.";
            return RedirectToAction("Index", "Home");
        }

        var items = HttpContext.Session.GetJson<List<CartItemViewModel>>(CartSessionKey) ?? [];
        var existing = items.FirstOrDefault(x => x.ProductId == productId);

        if (existing is null)
        {
            items.Add(new CartItemViewModel
            {
                ProductId = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                UnitPrice = product.Price,
                Quantity = quantity
            });
        }
        else
        {
            existing.Quantity += quantity;
        }

        HttpContext.Session.SetJson(CartSessionKey, items);
        TempData["Success"] = "Produs adaugat in cos.";

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int productId)
    {
        var items = HttpContext.Session.GetJson<List<CartItemViewModel>>(CartSessionKey) ?? [];
        items.RemoveAll(x => x.ProductId == productId);
        HttpContext.Session.SetJson(CartSessionKey, items);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        HttpContext.Session.Remove(CartSessionKey);
        return RedirectToAction(nameof(Index));
    }
}
