using MediatR;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Application.Queries.Order;

namespace PizzaOrdering.Application.Handlers.Order;

public class GetOrdersByCustomerNameHandler : IRequestHandler<GetOrdersByCustomerNameQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByCustomerNameHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByCustomerNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetByCustomerNameAsync(request.CustomerName, cancellationToken);
        
        return orders.Select(order => new OrderDto
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
        });
    }
} 