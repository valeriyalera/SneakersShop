namespace SneakersShop.Models;

public class Color
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Зв'язок багато-до-багатьох через проміжну таблицю
    public ICollection<SneakerColor> SneakerColors { get; set; } = new List<SneakerColor>();
}