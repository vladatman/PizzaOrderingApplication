using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaOrdering.Application.DTOs;
using PizzaOrdering.Application.Queries.Pizza;

namespace PizzaOrdering.API.Controllers
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly ISender _sender;

        public PizzaController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Get pizzas with optional max price filtering
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaDto>>> GetPizzas(
            [FromQuery] decimal? maxPrice, 
            CancellationToken cancellationToken)
        {
            var pizzas = await _sender.Send(new GetPizzasQuery(maxPrice), cancellationToken);
            return Ok(pizzas);
        }

        /// <summary>
        /// Get pizza by ID with size-based pricing
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PizzaDto>> GetPizzaById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var pizza = await _sender.Send(new GetPizzaByIdQuery(id), cancellationToken);
                return Ok(pizza);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Pizza with ID {id} not found.");
            }
        }

        /// <summary>
        /// Get all pizzas with size-based pricing and default toppings, with optional topping filtering
        /// </summary>
        [HttpGet("with-toppings")]
        public async Task<ActionResult<IEnumerable<PizzaDto>>> GetPizzasWithToppings(
            [FromQuery] int? toppingId, 
            CancellationToken cancellationToken)
        {
            var pizzas = await _sender.Send(new GetPizzasWithToppingsQuery(toppingId), cancellationToken);
            return Ok(pizzas);
        }

        /// <summary>
        /// Get pizza by ID with size-based pricing and default toppings
        /// </summary>
        [HttpGet("with-toppings/{id:int}")]
        public async Task<ActionResult<PizzaDto>> GetPizzaByIdWithDefaultToppings(int id, CancellationToken cancellationToken)
        {
            try
            {
                var pizza = await _sender.Send(new GetPizzaByIdWithDefaultToppingsQuery(id), cancellationToken);
                return Ok(pizza);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Pizza with ID {id} not found.");
            }
        }
    }
}
