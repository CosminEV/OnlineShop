using Microsoft.EntityFrameworkCore;
using MagazineOnline.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// GET/POST/PUT/DELETE 
builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();

