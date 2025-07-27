using MediatR;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Application.Queries.Topping;

namespace PizzaOrdering.Application.Handlers.Topping;

public class GetToppingsHandler : IRequestHandler<GetToppingsQuery, IEnumerable<ToppingDto>>
{
    private readonly IToppingRepository _toppingRepository;

    public GetToppingsHandler(IToppingRepository toppingRepository)
    {
        _toppingRepository = toppingRepository;
    }

    public async Task<IEnumerable<ToppingDto>> Handle(GetToppingsQuery request, CancellationToken cancellationToken)
    {
        var toppings = await _toppingRepository.GetAllAsync(cancellationToken);
        
        return toppings.Select(topping => new ToppingDto
        {
            Id = topping.Id,
            Name = topping.Name,
            Description = topping.Description,
            Price = topping.Price,
            IsAvailable = topping.IsAvailable
        });
    }
} 