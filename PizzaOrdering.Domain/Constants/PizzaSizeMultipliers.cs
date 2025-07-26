using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Domain.Constants;

public static class PizzaSizeMultipliers
{
    public static readonly Dictionary<PizzaSize, decimal> Multipliers = new()
    {
        { PizzaSize.Small, 1.0m },    // Base price (no multiplier)
        { PizzaSize.Medium, 1.45m },  // 45% more than small
        { PizzaSize.Large, 1.89m }    // 89% more than small
    };
    
    public static decimal GetMultiplier(PizzaSize size)
    {
        return Multipliers.TryGetValue(size, out var multiplier) ? multiplier : 1.0m;
    }
    
    public static decimal CalculatePriceForSize(decimal basePrice, PizzaSize size)
    {
        return basePrice * GetMultiplier(size);
    }
} 