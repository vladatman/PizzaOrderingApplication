using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
} 