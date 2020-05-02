namespace WebAppSome.DataAccess.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        //public async Task<User> GetUserAsync(string email, string password)
        //{
        //    email = email ?? throw new ArgumentNullException(nameof(email));
        //    password = password ?? throw new ArgumentNullException(nameof(password));

        //    return await this.context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));
        //}

        //public async Task<User> GetUserByEmailAsync(string email)
        //{
        //    email = email ?? throw new ArgumentNullException(nameof(email));

        //    return await this.context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        //}

        //public async Task<User> CreateUserAsync(string email, string password)
        //{
        //    email = email ?? throw new ArgumentNullException(nameof(email));
        //    password = password ?? throw new ArgumentNullException(nameof(password));

        //    User usr = new User { Email = email, Password = password };
        //    this.context.Users.Add(usr);
            
        //    if(await this.context.SaveChangesAsync() > 0)
        //    {
        //        return usr;
        //    }

        //    throw new InvalidOperationException("Error creating user.");
        //}
    }
}
