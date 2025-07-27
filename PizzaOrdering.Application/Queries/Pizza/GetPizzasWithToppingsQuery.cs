using MediatR;
using PizzaOrdering.Application.DTOs;

namespace PizzaOrdering.Application.Queries.Pizza;

/// <summary>
/// Query to retrieve pizzas with their default toppings and optional filtering
/// </summary>
public record GetPizzasWithToppingsQuery(int? ToppingId = null) : IRequest<IEnumerable<PizzaDto>>; 