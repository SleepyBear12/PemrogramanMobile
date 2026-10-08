namespace BlazingPizzaApp.Model
{
    public class SavedOrder
    {
        public int OrderId { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<SavedOrderPizza> Pizzas { get; set; } = new List<SavedOrderPizza>();

        public decimal GetTotalPrice() => Pizzas.Sum(p => p.Price);

        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
    }

    public class SavedOrderPizza
    {
        public int SpecialId { get; set; }

        public string PizzaName { get; set; } = string.Empty;

        public int Size { get; set; }

        public decimal Price { get; set; }

        // Size now stores inches directly
        public int SizeInches => Size;

        public string GetFormattedPrice() => Price.ToString("0.00");
    }
}
