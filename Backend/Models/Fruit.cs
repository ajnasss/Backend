namespace Backend.Models;

public class Fruit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public double WaterRequirement { get; set; }
    public string? ImageUrl { get; set; }
}

