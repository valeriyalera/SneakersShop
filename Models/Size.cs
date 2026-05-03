namespace SneakersShop.Models;

public class Size
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Навігаційна властивість: один розмір може бути у багатьох кросівок
    public ICollection<Sneaker> Sneakers { get; set; } = new List<Sneaker>();
}