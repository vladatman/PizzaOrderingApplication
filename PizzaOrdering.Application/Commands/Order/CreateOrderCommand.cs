using MediatR;
using PizzaOrdering.Application.DTOs;

namespace PizzaOrdering.Application.Commands.Order;

/// <summary>
/// Command to create a new order
/// </summary>
public record CreateOrderCommand(CreateOrderRequest Request) : IRequest<OrderDto>; 