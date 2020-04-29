using Microsoft.EntityFrameworkCore;
namespace WebAppSome
{
    using WebAppSome.Entities;

    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
