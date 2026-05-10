namespace SneakersShop.Models;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Sneaker> Sneakers { get; set; } = new List<Sneaker>();
}