using Microsoft.EntityFrameworkCore;

namespace SneakersShop.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Sneaker> Sneakers { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<SneakerColor> SneakerColors { get; set; }
public DbSet<Size> Sizes { get; set; } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Налаштування зв'язків

        // Sneaker -> Brand (багато до одного)
        modelBuilder.Entity<Sneaker>()
            .HasOne(s => s.Brand)
            .WithMany(b => b.Sneakers)
            .HasForeignKey(s => s.BrandId)
            .OnDelete(DeleteBehavior.Cascade);

        // Sneaker -> Category
        modelBuilder.Entity<Sneaker>()
            .HasOne(s => s.Category)
            .WithMany(c => c.Sneakers)
            .HasForeignKey(s => s.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Sneaker>()
            .HasOne(s => s.Size)
            .WithMany(sz => sz.Sneakers)
            .HasForeignKey(s => s.SizeId)
            .OnDelete(DeleteBehavior.Cascade);


        // Зв'язок багато-до-багатьох через SneakerColor
        modelBuilder.Entity<SneakerColor>()
            .HasOne(sc => sc.Sneaker)
            .WithMany(s => s.SneakerColors)
            .HasForeignKey(sc => sc.SneakerId);

        modelBuilder.Entity<SneakerColor>()
            .HasOne(sc => sc.Color)
            .WithMany(c => c.SneakerColors)
            .HasForeignKey(sc => sc.ColorId);
    }
}