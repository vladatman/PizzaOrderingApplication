using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Queries.Topping;

namespace PizzaOrdering.API.Controllers;

[Route("api/toppings")]
[ApiController]
public class ToppingController : ControllerBase
{
    private readonly ISender _sender;

    public ToppingController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get all available toppings
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToppingDto>>> GetToppings(CancellationToken cancellationToken)
    {
        var toppings = await _sender.Send(new GetToppingsQuery(), cancellationToken);
        return Ok(toppings);
    }

    /// <summary>
    /// Get topping by ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ToppingDto>> GetToppingById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var topping = await _sender.Send(new GetToppingByIdQuery(id), cancellationToken);
            return Ok(topping);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Topping with ID {id} not found.");
        }
    }
} 