using MediatR;
using PizzaOrdering.Application.DTOs;

namespace PizzaOrdering.Application.Queries.Pizza;

public record GetPizzaByIdWithDefaultToppingsQuery(int Id) : IRequest<PizzaDto>; 