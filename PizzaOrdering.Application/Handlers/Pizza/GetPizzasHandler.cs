using MediatR;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Application.Queries.Pizza;
using PizzaOrdering.Domain.Constants;
using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.Application.Handlers.Pizza;

public class GetPizzasHandler : IRequestHandler<GetPizzasQuery, IEnumerable<PizzaDto>>
{
    private readonly IPizzaRepository _pizzaRepository;

    public GetPizzasHandler(IPizzaRepository pizzaRepository)
    {
        _pizzaRepository = pizzaRepository;
    }

    public async Task<IEnumerable<PizzaDto>> Handle(GetPizzasQuery request, CancellationToken cancellationToken)
    {
        var pizzas = await _pizzaRepository.GetAllAsync(cancellationToken);
        
        // Apply max price filter if provided
        if (request.MaxPrice.HasValue)
        {
            pizzas = pizzas.Where(p => 
                PizzaSizeMultipliers.CalculatePriceForSize(p.BasePrice, PizzaSize.Small) <= request.MaxPrice.Value ||
                PizzaSizeMultipliers.CalculatePriceForSize(p.BasePrice, PizzaSize.Medium) <= request.MaxPrice.Value ||
                PizzaSizeMultipliers.CalculatePriceForSize(p.BasePrice, PizzaSize.Large) <= request.MaxPrice.Value
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
            LargePrice = PizzaSizeMultipliers.CalculatePriceForSize(pizza.BasePrice, PizzaSize.Large)
        });
    }
} 