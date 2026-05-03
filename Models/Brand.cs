namespace SneakersShop.Models;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Навігаційна властивість (один бренд – багато кросівок)
    public ICollection<Sneaker> Sneakers { get; set; } = new List<Sneaker>();
}