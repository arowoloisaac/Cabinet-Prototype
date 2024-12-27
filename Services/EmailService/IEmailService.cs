namespace Cabinet_Prototype.Services.EmailService
{
    public interface IEmailService
    {
        Task<string> SendEmail(string email, string subject, string description);
    }
}
