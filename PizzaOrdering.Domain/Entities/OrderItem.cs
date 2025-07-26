namespace PizzaOrdering.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int PizzaId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    // Navigation properties
    public Order Order { get; set; } = null!;
    public Pizza Pizza { get; set; } = null!;
    public ICollection<OrderItemTopping> ExtraToppings { get; set; } = new List<OrderItemTopping>();
} 