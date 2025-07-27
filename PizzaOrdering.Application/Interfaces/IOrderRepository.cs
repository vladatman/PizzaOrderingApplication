using PizzaOrdering.Domain.Entities;

namespace PizzaOrdering.Application.Interfaces;

/// <summary>
/// Repository interface for order-related data access operations
/// </summary>
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetByCustomerNameAsync(string customerName, CancellationToken cancellationToken = default);
    Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default);
} 