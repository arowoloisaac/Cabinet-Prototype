
using Cabinet_Prototype.Configurations;
using System.Net;
using System.Net.Http;
using System.Net.Mail;

namespace Cabinet_Prototype.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public async Task<string> SendEmail(string toEmail, string subject, string description)
        {
            //var config = 
            EmailSettings settings = new EmailSettings();

            _configuration.GetSection("MailCredientials").Bind(settings);

            var senderEmail = settings.Email;
            var senderPwd = settings.Password;

            var em2 = settings.Email;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(senderEmail, senderPwd),
                Timeout = 20000,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = description,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);


            smtpClient.Send(mailMessage);

            return em2;
        }
    }
}
