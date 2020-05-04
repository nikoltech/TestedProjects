namespace WebAppSome.BusinessLogic.Services.Email
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task SendEmailAsync(Message message);
    }
}
