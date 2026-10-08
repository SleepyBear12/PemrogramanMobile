using BlazingPizzaApp.Model;
using System.Net.Http.Json;

namespace BlazingPizzaApp.Data
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SavedOrder> SaveOrderAsync(IEnumerable<SavedOrderPizza> pizzas)
        {
            var request = new SavedOrder
            {
                Pizzas = pizzas.ToList()
            };

            var response = await _httpClient.PostAsJsonAsync("api/Orders", request);
            response.EnsureSuccessStatusCode();

            var saved = await response.Content.ReadFromJsonAsync<SavedOrder>();
            return saved ?? throw new InvalidOperationException("The API returned an empty order.");
        }

        public async Task<List<SavedOrder>> GetOrdersAsync()
        {
            var orders = await _httpClient.GetFromJsonAsync<List<SavedOrder>>("api/Orders");
            return orders ?? new List<SavedOrder>();
        }
    }
}
