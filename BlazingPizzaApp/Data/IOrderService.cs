using BlazingPizzaApp.Model;

namespace BlazingPizzaApp.Data
{
    public interface IOrderService
    {
        Task<SavedOrder> SaveOrderAsync(IEnumerable<SavedOrderPizza> pizzas);

        Task<List<SavedOrder>> GetOrdersAsync();
    }
}
