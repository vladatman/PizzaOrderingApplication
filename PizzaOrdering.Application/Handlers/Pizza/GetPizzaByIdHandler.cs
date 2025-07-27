using MediatR;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Application.Queries.Pizza;
using PizzaOrdering.Domain.Constants;
using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Application.Handlers.Pizza;

public class GetPizzaByIdHandler : IRequestHandler<GetPizzaByIdQuery, PizzaDto>
{
    private readonly IPizzaRepository _pizzaRepository;
    
    public GetPizzaByIdHandler(IPizzaRepository pizzaRepository)
    {
        _pizzaRepository = pizzaRepository;
    }
    
    public async Task<PizzaDto> Handle(GetPizzaByIdQuery request, CancellationToken cancellationToken)
    {
        var pizza = await _pizzaRepository.GetByIdWithDefaultToppingsAsync(request.Id, cancellationToken);
        
        if (pizza is null)
        {
            throw new KeyNotFoundException($"Pizza with ID {request.Id} not found.");
        }
        
        return new PizzaDto
        {
            Id = pizza.Id,
            Name = pizza.Name,
            Description = pizza.Description,
            SmallPrice = PizzaSizeMultipliers.CalculatePriceForSize(pizza.BasePrice, PizzaSize.Small),
            MediumPrice = PizzaSizeMultipliers.CalculatePriceForSize(pizza.BasePrice, PizzaSize.Medium),
            LargePrice = PizzaSizeMultipliers.CalculatePriceForSize(pizza.BasePrice, PizzaSize.Large),
            DefaultToppings = pizza.DefaultToppings.Select(pt => new ToppingDto
            {
                Id = pt.Topping.Id,
                Name = pt.Topping.Name,
                Description = pt.Topping.Description,
                Price = pt.Topping.Price,
                IsAvailable = pt.Topping.IsAvailable
            }).ToList()
        };
    }
} 