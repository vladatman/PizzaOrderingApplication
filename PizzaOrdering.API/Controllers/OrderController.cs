using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaOrdering.Application.Commands.Order;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Queries.Order;
using PizzaOrdering.Domain.Enums;

namespace PizzaOrdering.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ISender _sender;

    public OrderController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get all orders with their items and toppings
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders(CancellationToken cancellationToken)
    {
        var orders = await _sender.Send(new GetOrdersQuery(), cancellationToken);
        return Ok(orders);
    }

    /// <summary>
    /// Get order by ID with all details
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _sender.Send(new GetOrderByIdQuery(id), cancellationToken);
            return Ok(order);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Order with ID {id} not found.");
        }
    }

    /// <summary>
    /// Get orders by customer name
    /// </summary>
    [HttpGet("customer/{customerName}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCustomerName(string customerName, CancellationToken cancellationToken)
    {
        var orders = await _sender.Send(new GetOrdersByCustomerNameQuery(customerName), cancellationToken);
        
        if (!orders.Any())
        {
            return NotFound($"No orders found for customer '{customerName}'.");
        }
        
        return Ok(orders);
    }

    /// <summary>
    /// Create a new order
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _sender.Send(new CreateOrderCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update order status
    /// </summary>
    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<OrderDto>> UpdateOrderStatus(int id, [FromBody] OrderStatus newStatus, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _sender.Send(new UpdateOrderStatusCommand(id, newStatus), cancellationToken);
            return Ok(order);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 