using Microsoft.EntityFrameworkCore;
using Test_Backend.Model;
namespace Test_Backend.Data
{
    public class AppDbContex:DbContext
    {
        public AppDbContex(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> customers { get; set; }
        public DbSet<User> users { get; set; }
    }
}
