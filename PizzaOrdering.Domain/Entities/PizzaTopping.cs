namespace PizzaOrdering.Domain.Entities;

public class PizzaTopping
{
    public int PizzaId { get; set; }
    public int ToppingId { get; set; }
    public bool IsDefault { get; set; } = true;

    // Navigation properties
    public Pizza Pizza { get; set; } = null!;
    public Topping Topping { get; set; } = null!;
} 