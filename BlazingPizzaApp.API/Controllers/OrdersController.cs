using BlazingPizzaApp.API.Data;
using BlazingPizzaApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizzaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PizzaStoreContext _db;

        public OrdersController(PizzaStoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _db.Orders
                .Include(o => o.Pizzas)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderRequest request)
        {
            if (request.Pizzas == null || request.Pizzas.Count == 0)
            {
                return BadRequest("An order must contain at least one pizza.");
            }

            var order = new Order();

            foreach (var item in request.Pizzas)
            {
                if (item.Size < 1 || item.Size > 3)
                {
                    return BadRequest($"Invalid pizza size: {item.Size}.");
                }

                var special = await _db.PizzaSpecials.FindAsync(item.SpecialId);
                if (special == null)
                {
                    return BadRequest($"Pizza special {item.SpecialId} does not exist.");
                }

                order.Pizzas.Add(new OrderPizza
                {
                    SpecialId = special.Id,
                    PizzaName = special.Name,
                    Size = item.Size,
                    Price = special.BasePrice * item.Size
                });
            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = order.OrderId }, order);
        }
    }

    public class CreateOrderRequest
    {
        public List<CreateOrderPizza> Pizzas { get; set; } = new List<CreateOrderPizza>();
    }

    public class CreateOrderPizza
    {
        public int SpecialId { get; set; }

        public int Size { get; set; }
    }
}
