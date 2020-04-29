namespace WebAppSome.Interfaces
{
    using System.Threading.Tasks;
    using WebAppSome.Entities;

    public interface IRepository
    {
        Task<User> GetUserAsync(string email, string password);

        Task<User> GetUserByEmailAsync(string email);

        Task<User> CreateUserAsync(string email, string password);
    }
}
