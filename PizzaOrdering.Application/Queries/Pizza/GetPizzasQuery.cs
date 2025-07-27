using MediatR;
using PizzaOrdering.Application.DTOs;

namespace PizzaOrdering.Application.Queries.Pizza;

/// <summary>
/// Query to retrieve pizzas with optional filtering
/// </summary>
public record GetPizzasQuery(decimal? MaxPrice = null) : IRequest<IEnumerable<PizzaDto>>; 