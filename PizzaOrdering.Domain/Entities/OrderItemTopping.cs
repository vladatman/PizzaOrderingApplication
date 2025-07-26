namespace PizzaOrdering.Domain.Entities;

public class OrderItemTopping
{
    public int OrderItemId { get; set; }
    public int ToppingId { get; set; }

    // Navigation properties
    public OrderItem OrderItem { get; set; } = null!;
    public Topping Topping { get; set; } = null!;
} 