namespace CRUDEF
{
    using CRUDEF.Entities.TPH;
    using Microsoft.EntityFrameworkCore;

    public class TPHContext : DbContext
    {
        public TPHContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<UserTPH> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=inheritedb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTPH>().HasQueryFilter(u => u.Age >= 17);
        }
    }
}
