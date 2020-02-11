namespace CRUDEF
{
    using CRUDEF.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<App2Context>();

            var options = optionsBuilder
                    .UseSqlServer(connectionString,
                        x => x.MigrationsAssembly("CRUDEF"))
                    .Options;
            /*
            // Добавление
            using (AppContext db = new AppContext(options))
            {
                User user1 = new User { Name = "Tom", Age = 33 };
                User user2 = new User { Name = "Alice", Age = 26 };

                // Добавление
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
            }

            // получение
            using (AppContext db = new AppContext(options))
            {
                // получаем объекты из бд и выводим на консоль
                var users = db.Users.ToList();
                Console.WriteLine("Данные после добавления:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }

            // Редактирование
            using (AppContext db = new AppContext(options))
            {
                // получаем первый объект
                User user = db.Users.FirstOrDefault();
                if (user != null)
                {
                    user.Name = "Bob";
                    user.Age = 44;
                    //обновляем объект
                    //db.Users.Update(user);
                    db.SaveChanges();
                }
                // выводим данные после обновления
                Console.WriteLine("\nДанные после редактирования:");
                var users = db.Users.ToList();
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }

            // Удаление
            using (AppContext db = new AppContext(options))
            {
                // получаем первый объект
                User user = db.Users.FirstOrDefault();
                if (user != null)
                {
                    //удаляем объект
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                // выводим данные после обновления
                Console.WriteLine("\nДанные после удаления:");
                var users = db.Users.ToList();
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }

                _ = db.Database.ExecuteSqlCommand("TRUNCATE TABLE Users");
            }
            */
            using (App2Context db = new App2Context(options))
            {
                try
                {
                    //db.GetService<ILoggerFactory>().AddProvider(new crudefDbLoggerProvider());
                    User user1 = new User { PassportSeria = "KM", PassportNumber = "123458", Name = "Tom", Age = 33 };
                    User user2 = new User { PassportSeria = "KM", PassportNumber = "123459", Name = "Alice", Age = 26 };

                    db.Users.Add(user1);
                    db.Users.Add(user2);
                    db.SaveChanges();

                    var users = db.Users.ToList();
                    Console.WriteLine("Данные после добавления:");
                    foreach (User u in users)
                    {
                        Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                    }

                    Console.WriteLine();
                    User user = new User { Name = "Tom" };
                    Console.WriteLine($"Id перед добавлением в контекст {user.Id}");    // Id = 0
                    db.Users.Add(user);
                    db.SaveChanges();
                    Console.WriteLine($"Id после добавления в базу данных {user.Id}");  // Id = 3
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] -- {ex.Message}");
                }

            }
            Console.Read();
        }
    }
}