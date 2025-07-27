using Microsoft.EntityFrameworkCore;
using PizzaOrdering.Application.Interfaces;
using PizzaOrdering.Domain.Entities;
using PizzaOrdering.Infrastructure.Data;

namespace PizzaOrdering.Infrastructure.Repositories;

public class OrderRepository(PizzaOrderingDbContext dbContext) : IOrderRepository
{
    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Orders
            .Include(o => o.Items)
                .ThenInclude(oi => oi.Pizza)
            .Include(o => o.Items)
                .ThenInclude(oi => oi.ExtraToppings)
                    .ThenInclude(oit => oit.Topping)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Orders
            .Include(o => o.Items)
                .ThenInclude(oi => oi.Pizza)
            .Include(o => o.Items)
                .ThenInclude(oi => oi.ExtraToppings)
                    .ThenInclude(oit => oit.Topping)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetByCustomerNameAsync(string customerName, CancellationToken cancellationToken = default)
    {
        return await dbContext.Orders
            .Include(o => o.Items)
                .ThenInclude(oi => oi.Pizza)
            .Include(o => o.Items)
                .ThenInclude(oi => oi.ExtraToppings)
                    .ThenInclude(oit => oit.Topping)
            .Where(o => o.CustomerName.Contains(customerName))
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        order.CreatedAt = DateTime.UtcNow;
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return order;
    }
} 