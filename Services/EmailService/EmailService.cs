
using System.Net.Mail;

namespace Cabinet_Prototype.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {
            
        }

        public Task SendEmail(string email, string subject, string description)
        {
            var stmpCLient = new SmtpClient("");
            throw new NotImplementedException();
        }
    }
}
