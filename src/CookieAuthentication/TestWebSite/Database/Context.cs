using Microsoft.EntityFrameworkCore;
using TestWebSite.Database.Models;

namespace TestWebSite.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }
    }
}
