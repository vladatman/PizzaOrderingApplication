namespace PizzaOrdering.Application.DTOs;

/// <summary>
/// DTO for topping information when selecting extra toppings
/// </summary>
public class ToppingDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
} 