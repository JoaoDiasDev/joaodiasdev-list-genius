using Microsoft.EntityFrameworkCore;

namespace JoaoDiasDev.ListGenius.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {

        }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsList> ProductsList { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
