namespace PizzaOrdering.Application.DTOs;

/// <summary>
/// Pizza information with size-based pricing and default toppings
/// </summary>
public class PizzaDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal SmallPrice { get; set; }
    public decimal MediumPrice { get; set; }
    public decimal LargePrice { get; set; }
    public List<ToppingDto> DefaultToppings { get; set; } = new();
} 