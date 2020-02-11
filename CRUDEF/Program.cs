namespace CRUDEF
{
    using CRUDEF.Entities;
    using CRUDEF.Entities.TPH;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
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

            #region First check Context
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
            #endregion

            try
            {
                using (App2Context db = new App2Context(options))
                {

                    ////db.GetService<ILoggerFactory>().AddProvider(new crudefDbLoggerProvider());
                    //User user1 = new User { PassportSeria = "KM", PassportNumber = "123458", Name = "Tom", Age = 33 };
                    //User user2 = new User { PassportSeria = "KM", PassportNumber = "123459", Name = "Alice", Age = 26 };

                    //db.Users.Add(user1);
                    //db.Users.Add(user2);
                    //db.SaveChanges();

                    //var users = db.Users.ToList();
                    //Console.WriteLine("Данные после добавления:");
                    //foreach (User u in users)
                    //{
                    //    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                    //}

                    //Console.WriteLine();
                    //User user = new User { Name = "Tom" };
                    //Console.WriteLine($"Id перед добавлением в контекст {user.Id}");    // Id = 0
                    //db.Users.Add(user);
                    //db.SaveChanges();
                    //Console.WriteLine($"Id после добавления в базу данных {user.Id}");  // Id = 3

                    //_ = db.Database.ExecuteSqlCommand("TRUNCATE TABLE Users");
                    //// добавляем начальные данные
                    //Company microsoft = new Company { Name = "Microsoft" };
                    //Company google = new Company { Name = "Google" };
                    //db.Companies.AddRange(microsoft, google);
                    //db.SaveChanges();
                    //User tom = new User { Name = "Tom", Company = microsoft };
                    //User bob = new User { Name = "Bob", Company = google };
                    //User alice = new User { Name = "Alice", Company = microsoft };
                    //User kate = new User { Name = "Kate", Company = google };
                    //db.Users.AddRange(tom, bob, alice, kate);
                    //db.SaveChanges();


                    //var companies = db.Companies.ToList();
                    //foreach (var c in companies) Console.WriteLine($"{c.Name}");
                    //Console.WriteLine();

                    //// получаем пользователей
                    //var users = db.Users
                    //    .Include(u => u.Company)  // добавляем данные по компаниям
                    //    .ToList();
                    //foreach (var user in users)
                    //    Console.WriteLine($"{user.Name} - {user.Company?.Name}");

                    //Console.WriteLine();
                    //User user1 = db.Users.FirstOrDefault(p => p.Name == "Bob");
                    //if (user1 != null)
                    //{
                    //    db.Users.Remove(user1);
                    //    db.SaveChanges();
                    //}

                    //Company comp = db.Companies.FirstOrDefault();
                    //if (comp != null)
                    //{
                    //    db.Companies.Remove(comp);
                    //    db.SaveChanges();
                    //}

                    //// Удаляем первую компанию
                    ////var comp = db.Companies.FirstOrDefault();
                    ////db.Companies.Remove(comp);
                    ////db.SaveChanges();
                    ////Console.WriteLine("\nСписок пользователей после удаления компании");
                    //// снова получаем пользователей
                    //users = db.Users.ToList();
                    //foreach (var user in users) Console.WriteLine($"{user.Name}");



                    /*

                    Student s1 = new Student { Name = "Tom" };
                    Student s2 = new Student { Name = "Alice" };
                    Student s3 = new Student { Name = "Bob" };
                    db.Students.AddRange(new List<Student> { s1, s2, s3 });

                    Course c1 = new Course { Name = "Алгоритмы" };
                    Course c2 = new Course { Name = "Основы программирования" };
                    db.Courses.AddRange(new List<Course> { c1, c2 });

                    db.SaveChanges();

                    // добавляем к студентам курсы
                    s1.StudentCourses.Add(new StudentCourse { CourseId = c1.Id, StudentId = s1.Id });
                    s2.StudentCourses.Add(new StudentCourse { CourseId = c1.Id, StudentId = s2.Id });
                    s2.StudentCourses.Add(new StudentCourse { CourseId = c2.Id, StudentId = s2.Id });
                    db.SaveChanges();*/

                    //Student student = db.Students.FirstOrDefault();
                    //db.Students.Remove(student);
                    //db.SaveChanges();

                    var courses = db.Courses.Include(c => c.StudentCourses).ThenInclude(sc => sc.Student).ToList();
                    // выводим все курсы
                    foreach (var c in courses)
                    {
                        Console.WriteLine($"\n Course: {c.Name}");
                        // выводим всех студентов для данного кура
                        var students = c.StudentCourses.Select(sc => sc.Student).ToList();
                        foreach (Student s in students)
                            Console.WriteLine($"{s.Name}");
                    }
                }
                using (TPHContext db = new TPHContext())
                {
                    UserTPH user1 = new UserTPH { Name = "Tom" };
                    UserTPH user2 = new UserTPH { Name = "Bob" };
                    db.Users.Add(user1);
                    db.Users.Add(user2);

                    Employee employee = new Employee { Name = "Sam", Salary = 500 };
                    db.Employees.Add(employee);

                    Manager manager = new Manager { Name = "Robert", Departament = "IT" };
                    db.Managers.Add(manager);

                    db.SaveChanges();

                    var users = db.Users.ToList();
                    Console.WriteLine("Все пользователи");
                    foreach (var user in users)
                    {
                        Console.WriteLine(user.Name);
                    }

                    Console.WriteLine("\n Все работники");
                    foreach (var emp in db.Employees.ToList())
                    {
                        Console.WriteLine(emp.Name);
                    }

                    Console.WriteLine("\nВсе менеджеры");
                    foreach (var man in db.Managers.ToList())
                    {
                        Console.WriteLine(man.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR Message] -- {ex.Message}");
                Console.WriteLine($"[ERROR InnerException] -- {ex.InnerException}");
            }

            Console.Read();
        }
    }
}