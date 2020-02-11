namespace CRUDEF
{
    using CRUDEF.Entities;
    using CRUDEF.Providers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class App2Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        public App2Context()
        { }

        public App2Context(DbContextOptions<App2Context> options)
            : base(options)
        {
             //Database.EnsureDeleted();
             Database.EnsureCreated();
        }
        /*
        private ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            //builder.AddProvider(new crudefDbLoggerProvider());    // указываем наш провайдер логгирования
            //builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
            //            && level == LogLevel.Information)
            //       .AddProvider(new crudefDbLoggerProvider());
        });
        */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasKey(u => new { u.PassportSeria, u.PassportNumber });
            modelBuilder.Entity<StudentCourse>()
                .HasKey(t => new { t.StudentId, t.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
