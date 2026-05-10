using Microsoft.EntityFrameworkCore;
using SneakersShop.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Hello World! Sneakers API is running.");

app.Run();