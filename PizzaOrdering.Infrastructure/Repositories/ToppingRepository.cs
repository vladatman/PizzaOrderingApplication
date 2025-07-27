using Microsoft.EntityFrameworkCore;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Domain.Entities;
using PizzaOrdering.Infrastructure.Data;

namespace PizzaOrdering.Infrastructure.Repositories;

public class ToppingRepository(PizzaOrderingDbContext dbContext) : IToppingRepository
{
    public async Task<IEnumerable<Topping>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Toppings.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Topping>> GetAvailableAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Toppings
            .Where(t => t.IsAvailable)
            .ToListAsync(cancellationToken);
    }

    public async Task<Topping?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Toppings.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Topping>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        return await dbContext.Toppings
            .Where(t => ids.Contains(t.Id))
            .ToListAsync(cancellationToken);
    }
} 