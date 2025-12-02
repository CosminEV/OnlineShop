using Microsoft.EntityFrameworkCore;
using MagazineOnline.Api.Models;

namespace MagazineOnline.Api.Data
{
    public class AppDbContext : DbContext
    {
        //AppDbContext este un tip special de clasa care mosteneste comportamentul de la DbContext
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}