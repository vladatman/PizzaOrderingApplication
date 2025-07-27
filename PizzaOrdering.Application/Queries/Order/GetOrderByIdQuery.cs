using MediatR;
using PizzaOrdering.Application.DTOs;

namespace PizzaOrdering.Application.Queries.Order;

/// <summary>
/// Query to retrieve a specific order by ID
/// </summary>
/// <param name="Id">The unique identifier of the order to retrieve</param>
/// <returns>An OrderDto containing the order information with all items and toppings</returns>
/// <exception cref="KeyNotFoundException">Thrown when an order with the specified ID is not found</exception>
public record GetOrderByIdQuery(int Id) : IRequest<OrderDto>; 