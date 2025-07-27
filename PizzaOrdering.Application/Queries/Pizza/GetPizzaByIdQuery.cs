using MediatR;
using PizzaOrdering.Application.DTOs;

namespace PizzaOrdering.Application.Queries.Pizza;

/// <summary>
/// Query to retrieve pizza with calculated pricing by ID
/// </summary>
/// <param name="Id">The unique identifier of the pizza to retrieve</param>
/// <returns>A PizzaDto containing the pizza information with calculated prices for all sizes</returns>
/// <exception cref="KeyNotFoundException">Thrown when a pizza with the specified ID is not found</exception>
public record GetPizzaByIdQuery(int Id) : IRequest<PizzaDto>; 