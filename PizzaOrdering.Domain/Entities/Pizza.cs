using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Domain.Entities;

public class Pizza
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public PizzaSize Size { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<PizzaTopping> DefaultToppings { get; set; } = new List<PizzaTopping>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
} 