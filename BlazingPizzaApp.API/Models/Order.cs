namespace BlazingPizzaApp.API.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<OrderPizza> Pizzas { get; set; } = new List<OrderPizza>();
    }
}
