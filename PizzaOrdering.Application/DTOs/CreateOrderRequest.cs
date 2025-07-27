namespace PizzaOrdering.Application.DTOs;

/// <summary>
/// Request DTO for creating a new order
/// </summary>
public class CreateOrderRequest
{
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string? DeliveryAddress { get; set; }
    public string? Notes { get; set; }
    public List<CreateOrderItemDto> Items { get; set; } = new();
} 