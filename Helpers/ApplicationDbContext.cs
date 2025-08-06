using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using core8_vue_mysql.Entities;

namespace core8_vue_mysql.Helpers
{
   public class ApplicationDbContext : DbContext
    {        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }

}