using PizzaOrdering.Domain.Entities;

namespace PizzaOrdering.Application.Interfaces;

public interface IToppingRepository
{
    Task<IEnumerable<Topping>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Topping>> GetAvailableAsync(CancellationToken cancellationToken = default);
    Task<Topping?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Topping>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
} 