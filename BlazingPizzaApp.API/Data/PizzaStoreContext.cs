using BlazingPizzaApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizzaApp.API.Data
{
    public class PizzaStoreContext : DbContext
    {
        public PizzaStoreContext(DbContextOptions<PizzaStoreContext> options)
            : base(options)
        {
        }

        public DbSet<PizzaSpecial> PizzaSpecials { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderPizza> OrderPizzas { get; set; }
    }
}
