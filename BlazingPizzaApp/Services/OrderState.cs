using BlazingPizzaApp.Model;

namespace BlazingPizzaApp.Services
{
    public class OrderState
    {
        public event Action? OnChange;

        // Orders persisted on server
        public List<SavedOrder> SavedOrders { get; } = new List<SavedOrder>();

        public void AddSavedOrder(SavedOrder order)
        {
            SavedOrders.Add(order);
            NotifyStateChanged();
        }

        public decimal GetTotalSavedPrice() => SavedOrders.Sum(o => o.GetTotalPrice());

        // Temporary cart stored on client-side
        public List<SavedOrderPizza> CartPizzas { get; } = new List<SavedOrderPizza>();

        public void AddPizzaToCart(SavedOrderPizza pizza)
        {
            CartPizzas.Add(pizza);
            NotifyStateChanged();
        }

        public void RemovePizzaFromCart(SavedOrderPizza pizza)
        {
            CartPizzas.Remove(pizza);
            NotifyStateChanged();
        }

        public void ClearCart()
        {
            CartPizzas.Clear();
            NotifyStateChanged();
        }

        public decimal GetCartTotal() => CartPizzas.Sum(p => p.Price);

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}
