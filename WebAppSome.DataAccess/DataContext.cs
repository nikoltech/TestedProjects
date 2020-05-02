namespace WebAppSome.DataAccess
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using WebAppSome.DataAccess.Entities;

    public class DataContext : IdentityDbContext<User>
    {


        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
