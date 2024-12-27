namespace Cabinet_Prototype.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string description);
    }
}
