using MagazineOnline.Api.Data;
using MagazineOnline.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
    options.SlidingExpiration = true;
});

var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
if (!string.IsNullOrWhiteSpace(googleClientId) && !string.IsNullOrWhiteSpace(googleClientSecret))
{
    builder.Services.AddAuthentication().AddGoogle(options =>
    {
        options.ClientId = googleClientId;
        options.ClientSecret = googleClientSecret;
    });
}

var facebookAppId = builder.Configuration["Authentication:Facebook:AppId"];
var facebookAppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
if (!string.IsNullOrWhiteSpace(facebookAppId) && !string.IsNullOrWhiteSpace(facebookAppSecret))
{
    builder.Services.AddAuthentication().AddFacebook(options =>
    {
        options.AppId = facebookAppId;
        options.AppSecret = facebookAppSecret;
    });
}

var githubClientId = builder.Configuration["Authentication:GitHub:ClientId"];
var githubClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
if (!string.IsNullOrWhiteSpace(githubClientId) && !string.IsNullOrWhiteSpace(githubClientSecret))
{
    builder.Services.AddAuthentication().AddGitHub(options =>
    {
        options.ClientId = githubClientId;
        options.ClientSecret = githubClientSecret;
        options.Scope.Add("user:email");
    });
}

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "OnlineShop.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromHours(2);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Products.Any())
    {
        dbContext.Products.AddRange(
            new Product
            {
                Name = "Casti Wireless Pro",
                Description = "Anulare activa de zgomot, baterie 30h si sunet premium.",
                Price = 349.99m,
                Stock = 20,
                Category = "Audio",
                IsFeatured = true,
                ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=1200"
            },
            new Product
            {
                Name = "Laptop Ultrabook X",
                Description = "Procesor puternic, SSD rapid, design slim pentru productivitate.",
                Price = 4299.00m,
                Stock = 8,
                Category = "Laptopuri",
                IsFeatured = true,
                ImageUrl = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=1200"
            },
            new Product
            {
                Name = "Smartwatch Fit",
                Description = "Monitorizare fitness, puls si GPS cu autonomie extinsa.",
                Price = 899.50m,
                Stock = 16,
                Category = "Wearables",
                IsFeatured = true,
                ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=1200"
            },
            new Product
            {
                Name = "Camera 4K",
                Description = "Filmare 4K stabilizata pentru creatori de continut.",
                Price = 2799m,
                Stock = 6,
                Category = "Foto-Video",
                ImageUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=1200"
            }
        );
        dbContext.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
