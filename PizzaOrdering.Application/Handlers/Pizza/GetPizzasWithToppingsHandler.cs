using MediatR;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Application.Queries.Pizza;
using PizzaOrdering.Domain.Constants;
using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Application.Handlers.Pizza;

public class GetPizzasWithToppingsHandler : IRequestHandler<GetPizzasWithToppingsQuery, IEnumerable<PizzaDto>>
{
    private readonly IPizzaRepository _pizzaRepository;

    public GetPizzasWithToppingsHandler(IPizzaRepository pizzaRepository)
    {
        _pizzaRepository = pizzaRepository;
    }

    public async Task<IEnumerable<PizzaDto>> Handle(GetPizzasWithToppingsQuery request, CancellationToken cancellationToken)
    {
        var pizzas = await _pizzaRepository.GetWithDefaultToppingsAsync(cancellationToken);

        // Apply topping filter if provided
        if (request.ToppingId.HasValue)
        {
            pizzas = pizzas.Where(p => 
                p.DefaultToppings.Any(pt => pt.ToppingId == request.ToppingId.Value)
            );
        }

        //Convert into PizzaDto
        return pizzas.Select(pizza => new PizzaDto
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
        });
    }
} 