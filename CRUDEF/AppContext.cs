namespace CRUDEF
{
    using CRUDEF.Entities;
    using CRUDEF.Providers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        private ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            //builder.AddProvider(new crudefDbLoggerProvider());    // указываем наш провайдер логгирования
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                        && level == LogLevel.Information)
                   .AddProvider(new crudefDbLoggerProvider());
        });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }
    }
}
