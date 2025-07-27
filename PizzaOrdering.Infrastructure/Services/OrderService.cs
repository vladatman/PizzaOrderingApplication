using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Domain.Constants;
using PizzaOrdering.Domain.Entities;
using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IToppingRepository _toppingRepository;

    public OrderService(IPizzaRepository pizzaRepository, IToppingRepository toppingRepository)
    {
        _pizzaRepository = pizzaRepository;
        _toppingRepository = toppingRepository;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        // Validate request
        var validationErrors = await ValidateOrderAsync(request, cancellationToken);
        if (validationErrors.Any())
        {
            throw new InvalidOperationException($"Order validation failed: {string.Join(", ", validationErrors)}");
        }

        // Create order
        var order = new Order
        {
            CustomerName = request.CustomerName.Trim(),
            CustomerPhone = request.CustomerPhone.Trim(),
            DeliveryAddress = request.DeliveryAddress?.Trim(),
            Notes = request.Notes?.Trim(),
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        var orderItems = new List<OrderItem>();
        decimal totalOrderPrice = 0;

        foreach (var itemDto in request.Items)
        {
            var pizza = await _pizzaRepository.GetByIdAsync(itemDto.PizzaId, cancellationToken);
            if (pizza == null)
                throw new InvalidOperationException($"Pizza with ID {itemDto.PizzaId} not found.");

            // Calculate prices
            var (unitPrice, extraToppings) = await CalculateItemPriceAsync(pizza, itemDto.Size, itemDto.ExtraToppingIds, cancellationToken);
            var totalPrice = unitPrice * itemDto.Quantity;

            var orderItem = new OrderItem
            {
                PizzaId = itemDto.PizzaId,
                Size = itemDto.Size,
                Quantity = itemDto.Quantity,
                UnitPrice = unitPrice,
                TotalPrice = totalPrice
            };

            // Add extra toppings
            foreach (var topping in extraToppings)
            {
                orderItem.ExtraToppings.Add(new OrderItemTopping
                {
                    ToppingId = topping.Id
                });
            }

            orderItems.Add(orderItem);
            totalOrderPrice += totalPrice;
        }

        order.Items = orderItems;
        order.TotalPrice = totalOrderPrice;

        return order;
    }

    private async Task<List<string>> ValidateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var errors = new List<string>();

        // Validate customer information
        if (string.IsNullOrWhiteSpace(request.CustomerName))
            errors.Add("Customer name is required.");
        
        if (string.IsNullOrWhiteSpace(request.CustomerPhone))
            errors.Add("Customer phone is required.");

        // Validate items
        if (request.Items == null || !request.Items.Any())
        {
            errors.Add("Order must contain at least one item.");
            return errors;
        }

        // Validate each item
        for (int i = 0; i < request.Items.Count; i++)
        {
            var item = request.Items[i];
            var itemIndex = i + 1;

            if (item.PizzaId <= 0)
                errors.Add($"Item {itemIndex}: Invalid pizza ID.");

            if (item.Quantity <= 0)
                errors.Add($"Item {itemIndex}: Quantity must be greater than 0.");

            if (item.Quantity > 10)
                errors.Add($"Item {itemIndex}: Quantity cannot exceed 10.");

            // Validate pizza exists
            var pizza = await _pizzaRepository.GetByIdAsync(item.PizzaId, cancellationToken);
            if (pizza == null)
                errors.Add($"Item {itemIndex}: Pizza with ID {item.PizzaId} not found.");

            // Validate extra toppings exist and are available
            if (item.ExtraToppingIds.Any())
            {
                var toppings = await _toppingRepository.GetAllAsync(cancellationToken);
                var selectedToppings = toppings.Where(t => item.ExtraToppingIds.Contains(t.Id)).ToList();
                
                foreach (var toppingId in item.ExtraToppingIds)
                {
                    var topping = selectedToppings.FirstOrDefault(t => t.Id == toppingId);
                    if (topping == null)
                        errors.Add($"Item {itemIndex}: Topping with ID {toppingId} not found.");
                    else if (!topping.IsAvailable)
                        errors.Add($"Item {itemIndex}: Topping '{topping.Name}' is not available.");
                }
            }
        }

        return errors;
    }

    private async Task<(decimal UnitPrice, List<Topping> ExtraToppings)> CalculateItemPriceAsync(Pizza pizza, PizzaSize size, List<int> extraToppingIds, CancellationToken cancellationToken)
    {
        // Calculate base price for the pizza size
        var basePrice = PizzaSizeMultipliers.CalculatePriceForSize(pizza.BasePrice, size);
        
        // Calculate extra toppings price
        decimal extraToppingsPrice = 0;
        var extraToppings = new List<Topping>();
        
        if (extraToppingIds.Any())
        {
            var toppings = await _toppingRepository.GetAllAsync(cancellationToken);
            extraToppings = toppings.Where(t => extraToppingIds.Contains(t.Id)).ToList();
            extraToppingsPrice = extraToppings.Sum(t => t.Price);
        }

        // Calculate unit price (base price + extra toppings)
        var unitPrice = basePrice + extraToppingsPrice;

        return (unitPrice, extraToppings);
    }
} 