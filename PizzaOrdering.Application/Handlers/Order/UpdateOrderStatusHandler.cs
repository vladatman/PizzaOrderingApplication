using MediatR;
using PizzaOrdering.Application.Commands.Order;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Application.Handlers.Order;

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderStatusHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        // Get the order
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {request.OrderId} not found.");
        }

        // Validate status transition
        if (!IsValidStatusTransition(order.Status, request.NewStatus))
        {
            throw new InvalidOperationException($"Cannot transition order from '{order.Status}' to '{request.NewStatus}'.");
        }

        // Update the status
        order.Status = request.NewStatus;
        
        // Set CompletedAt if order is being delivered
        if (request.NewStatus == OrderStatus.Delivered)
        {
            order.CompletedAt = DateTime.UtcNow;
        }

        // Save the updated order
        var updatedOrder = await _orderRepository.UpdateAsync(order, cancellationToken);

        // Convert to DTO for response
        return new OrderDto
        {
            Id = updatedOrder.Id,
            CustomerName = updatedOrder.CustomerName,
            CustomerPhone = updatedOrder.CustomerPhone,
            Status = updatedOrder.Status,
            DeliveryAddress = updatedOrder.DeliveryAddress,
            Notes = updatedOrder.Notes,
            TotalPrice = updatedOrder.TotalPrice,
            CreatedAt = updatedOrder.CreatedAt,
            CompletedAt = updatedOrder.CompletedAt,
            Items = updatedOrder.Items.Select(item => new OrderItemDto
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

    private static bool IsValidStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
    {
        return newStatus switch
        {
            OrderStatus.Pending => currentStatus == OrderStatus.Pending, // Can only stay pending
            OrderStatus.InProgress => currentStatus == OrderStatus.Pending, // Pending -> InProgress
            OrderStatus.Ready => currentStatus == OrderStatus.InProgress, // InProgress -> Ready
            OrderStatus.Delivered => currentStatus == OrderStatus.Ready, // Ready -> Delivered
            OrderStatus.Cancelled => currentStatus is OrderStatus.Pending or OrderStatus.InProgress, // Can cancel from Pending or InProgress
            _ => false
        };
    }
} 