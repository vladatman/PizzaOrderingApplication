using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Application.DTOs;

/// <summary>
/// DTO for creating order items
/// </summary>
public class CreateOrderItemDto
{
    public int PizzaId { get; set; }
    public PizzaSize Size { get; set; }
    public int Quantity { get; set; }
    public List<int> ExtraToppingIds { get; set; } = new();
} 