using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Orders.Db
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> orders { get; set; }

        public OrderDbContext(DbContextOptions options) : base(options) 
        {

        }
    }
}
