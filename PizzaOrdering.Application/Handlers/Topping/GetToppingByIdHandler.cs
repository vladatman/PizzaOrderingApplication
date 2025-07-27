using MediatR;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Application.Queries.Topping;

namespace PizzaOrdering.Application.Handlers.Topping;

public class GetToppingByIdHandler : IRequestHandler<GetToppingByIdQuery, ToppingDto>
{
    private readonly IToppingRepository _toppingRepository;

    public GetToppingByIdHandler(IToppingRepository toppingRepository)
    {
        _toppingRepository = toppingRepository;
    }

    public async Task<ToppingDto> Handle(GetToppingByIdQuery request, CancellationToken cancellationToken)
    {
        var topping = await _toppingRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (topping == null)
        {
            throw new KeyNotFoundException($"Topping with ID {request.Id} not found.");
        }

        return new ToppingDto
        {
            Id = topping.Id,
            Name = topping.Name,
            Description = topping.Description,
            Price = topping.Price,
            IsAvailable = topping.IsAvailable
        };
    }
} 