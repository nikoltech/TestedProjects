namespace WebAppSome.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using WebAppSome.DataAccess.Entities;

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
