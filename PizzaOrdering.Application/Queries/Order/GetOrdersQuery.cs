using MediatR;
using PizzaOrdering.Application.DTOs;

namespace PizzaOrdering.Application.Queries.Order;

/// <summary>
/// Query to retrieve all orders with optional filtering
/// </summary>
public record GetOrdersQuery : IRequest<IEnumerable<OrderDto>>; 