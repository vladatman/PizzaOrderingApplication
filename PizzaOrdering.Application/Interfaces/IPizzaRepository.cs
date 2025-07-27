using PizzaOrdering.Domain.Entities;

namespace PizzaOrdering.Application.Interfaces;

public interface IPizzaRepository
{
    Task<IEnumerable<Pizza>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Pizza?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Pizza>> GetWithDefaultToppingsAsync(CancellationToken cancellationToken = default);
    Task<Pizza?> GetByIdWithDefaultToppingsAsync(int id, CancellationToken cancellationToken = default);
} 