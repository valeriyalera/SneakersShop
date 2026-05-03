namespace SneakersShop.Models;

public class Sneaker
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int SizeId { get; set; }   // це зовнішній ключ
    public int CategoryId { get; set; }
    
    // Навігаційні властивості
    public Brand Brand { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public Size Size { get; set; } = null!;           // додаємо
    public ICollection<SneakerColor> SneakerColors { get; set; } = new List<SneakerColor>();
}