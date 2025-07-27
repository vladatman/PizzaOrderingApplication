using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Domain.Entities;

namespace PizzaOrdering.Application.Interfaces;

/// <summary>
/// Service for order-related business logic
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Creates a new order with validation and price calculation
    /// </summary>
    /// <param name="request">Order creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created order entity</returns>
    Task<Order> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
} 