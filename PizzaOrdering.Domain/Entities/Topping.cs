namespace PizzaOrdering.Domain.Entities;

public class Topping
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<PizzaTopping> PizzaToppings { get; set; } = new List<PizzaTopping>();
    public ICollection<OrderItemTopping> OrderItemToppings { get; set; } = new List<OrderItemTopping>();
} 