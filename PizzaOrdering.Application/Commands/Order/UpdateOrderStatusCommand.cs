using MediatR;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Application.Commands.Order;

/// <summary>
/// Command to update the status of an existing order
/// </summary>
/// <param name="OrderId">The ID of the order to update</param>
/// <param name="NewStatus">The new status to set</param>
public record UpdateOrderStatusCommand(int OrderId, OrderStatus NewStatus) : IRequest<OrderDto>; 