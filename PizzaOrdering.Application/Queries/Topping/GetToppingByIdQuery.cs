using MediatR;
using PizzaOrdering.Application.DTOs;

namespace PizzaOrdering.Application.Queries.Topping;

/// <summary>
/// Query to retrieve a specific topping by ID
/// </summary>
/// <param name="Id">The unique identifier of the topping to retrieve</param>
/// <returns>A ToppingDto containing the topping information</returns>
/// <exception cref="KeyNotFoundException">Thrown when a topping with the specified ID is not found</exception>
public record GetToppingByIdQuery(int Id) : IRequest<ToppingDto>; 