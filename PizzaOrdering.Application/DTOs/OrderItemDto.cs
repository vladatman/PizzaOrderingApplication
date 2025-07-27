using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Application.DTOs;

/// <summary>
/// Individual item within an order (pizza + size + quantity + extras)
/// </summary>
public class OrderItemDto
{
    public int Id { get; set; }
    public string PizzaName { get; set; } = string.Empty;
    public PizzaSize Size { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public List<ToppingDto> ExtraToppings { get; set; } = new();
}
