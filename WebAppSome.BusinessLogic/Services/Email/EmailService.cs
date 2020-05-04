namespace WebAppSome.BusinessLogic.Services.Email
{
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using System.Threading.Tasks;

    public class EmailService : IEmailService
    {
        private readonly EmailConfig EmailConfiguration;
        public EmailService(IOptions<EmailConfig> emailOptions)
        {
            this.EmailConfiguration = emailOptions.Value;
        }

        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            await SendAsync(emailMessage);
        }

        #region private methods
        private MimeMessage CreateEmailMessage(Message msg)
        {
            MimeMessage emailMsg = new MimeMessage();
            emailMsg.From.Add(new MailboxAddress(this.EmailConfiguration.From));
            emailMsg.To.AddRange(msg.To);
            emailMsg.Subject = msg.Subject;
            emailMsg.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = msg.Content };

            return emailMsg;
        }

        private async Task SendAsync(MimeMessage msg)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(
                        this.EmailConfiguration.SmtpServer,
                        this.EmailConfiguration.Port,
                        true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(this.EmailConfiguration.UserName, this.EmailConfiguration.Password);

                    await client.SendAsync(msg);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
        #endregion
    }
}
