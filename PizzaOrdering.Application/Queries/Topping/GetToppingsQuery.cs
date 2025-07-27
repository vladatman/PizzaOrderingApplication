using MediatR;
using PizzaOrdering.Application.DTOs;

namespace PizzaOrdering.Application.Queries.Topping;

/// <summary>
/// Query to retrieve all available toppings
/// </summary>
public record GetToppingsQuery : IRequest<IEnumerable<ToppingDto>>; 