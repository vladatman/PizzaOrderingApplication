using MediatR;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Application.Queries.Order;

namespace PizzaOrdering.Application.Handlers.Order;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {request.Id} not found.");
        }

        return new OrderDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            CustomerPhone = order.CustomerPhone,
            Status = order.Status,
            DeliveryAddress = order.DeliveryAddress,
            Notes = order.Notes,
            TotalPrice = order.TotalPrice,
            CreatedAt = order.CreatedAt,
            CompletedAt = order.CompletedAt,
            Items = order.Items.Select(item => new OrderItemDto
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