namespace SneakersShop.Models;

public class Sneaker
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int SizeId { get; set; }
    public int CategoryId { get; set; }
   
    public Brand? Brand { get; set; }
    public Category? Category { get; set; }
    public Size? Size { get; set; }
    public ICollection<SneakerColor>? SneakerColors { get; set; }
}