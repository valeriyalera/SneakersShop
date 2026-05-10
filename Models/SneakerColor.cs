namespace SneakersShop.Models;

public class SneakerColor
{
    public int Id { get; set; }
    public int SneakerId { get; set; }
    public int ColorId { get; set; }
    
    public Sneaker Sneaker { get; set; } = null!;
    public Color Color { get; set; } = null!;
}