using Microsoft.EntityFrameworkCore;
using SneakersShop.Models;

var builder = WebApplication.CreateBuilder(args);

// Зміни UseSqlite на UseNpgsql
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Hello World! Sneakers API with PostgreSQL");

app.Run();