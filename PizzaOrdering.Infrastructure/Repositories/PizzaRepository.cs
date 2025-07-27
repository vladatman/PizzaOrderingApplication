using Microsoft.EntityFrameworkCore;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Domain.Entities;
using PizzaOrdering.Infrastructure.Data;

namespace PizzaOrdering.Infrastructure.Repositories;

public class PizzaRepository(PizzaOrderingDbContext dbContext) : IPizzaRepository
{

    public async Task<IEnumerable<Pizza>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Pizzas.ToListAsync(cancellationToken);
    }

    public async Task<Pizza?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Pizzas.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Pizza>> GetWithDefaultToppingsAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Pizzas
            .Include(p => p.DefaultToppings)
                .ThenInclude(pt => pt.Topping)
            .ToListAsync(cancellationToken);
    }

    public async Task<Pizza?> GetByIdWithDefaultToppingsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Pizzas
            .Include(p => p.DefaultToppings)
                .ThenInclude(pt => pt.Topping)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
} 