namespace WebAppSome.DataAccess.Repositories
{
    using System.Threading.Tasks;
    using WebAppSome.DataAccess.Entities;

    public interface IRepository
    {
        Task<User> GetUserAsync(string email, string password);

        Task<User> GetUserByEmailAsync(string email);

        Task<User> CreateUserAsync(string email, string password);
    }
}
