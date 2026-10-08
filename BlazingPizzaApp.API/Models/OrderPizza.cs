namespace BlazingPizzaApp.API.Models
{
    public class OrderPizza
    {
        public int OrderPizzaId { get; set; }

        public int OrderId { get; set; }

        public int SpecialId { get; set; }

        public string PizzaName { get; set; } = string.Empty;

        public int Size { get; set; }

        public decimal Price { get; set; }
    }
}
