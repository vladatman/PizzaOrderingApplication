using MediatR;
using PizzaOrdering.Application.DTOs;

namespace PizzaOrdering.Application.Queries.Order;

/// <summary>
/// Query to retrieve orders by customer name (partial match)
/// </summary>
/// <param name="CustomerName">The customer name to search for</param>
/// <returns>A collection of OrderDto objects matching the customer name</returns>
public record GetOrdersByCustomerNameQuery(string CustomerName) : IRequest<IEnumerable<OrderDto>>; 