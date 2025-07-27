using MediatR;
using PizzaOrdering.Application.Commands.Order;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Interfaces;

namespace PizzaOrdering.Application.Handlers.Order;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderService _orderService;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderHandler(IOrderService orderService, IOrderRepository orderRepository)
    {
        _orderService = orderService;
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Create order using business logic service
        var order = await _orderService.CreateOrderAsync(request.Request, cancellationToken);
        
        // Save order to database
        var savedOrder = await _orderRepository.AddAsync(order, cancellationToken);
        
        // Convert to DTO for response
        return new OrderDto
        {
            Id = savedOrder.Id,
            CustomerName = savedOrder.CustomerName,
            CustomerPhone = savedOrder.CustomerPhone,
            Status = savedOrder.Status,
            DeliveryAddress = savedOrder.DeliveryAddress,
            Notes = savedOrder.Notes,
            TotalPrice = savedOrder.TotalPrice,
            CreatedAt = savedOrder.CreatedAt,
            CompletedAt = savedOrder.CompletedAt,
            Items = savedOrder.Items.Select(item => new OrderItemDto
            {
                Id = item.Id,
                PizzaName = item.Pizza.Name,
                Size = item.Size,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                ExtraToppings = item.ExtraToppings.Select(et => new ToppingDto
                {
                    Id = et.Topping.Id,
                    Name = et.Topping.Name,
                    Description = et.Topping.Description,
                    Price = et.Topping.Price,
                    IsAvailable = et.Topping.IsAvailable
                }).ToList()
            }).ToList()
        };
    }
} 