using Cabinet_Prototype.Data;
using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Models;
using Cabinet_Prototype.Services.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Cabinet_Prototype.Services.BackgroundJobs
{
    public class BackgroundJobs : IJob
    {
        private readonly IEmailService emailService;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> _userManager;

        private DateTime CurrentTime {  get; set; }

        public BackgroundJobs(IEmailService emailService, ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.emailService = emailService;
            this.dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            CurrentTime = DateTime.UtcNow;
            var retrieveRequests = await dbContext.UserRequests.Where(req => req.RequestStatus == RequestStatus.isPending).ToListAsync();


            if (retrieveRequests.Any(reqTime =>  reqTime.RegisteredTime >= CurrentTime.AddHours(-2) || retrieveRequests.Count >= 10 ))
            {
                // add the administator's email when test
                var getCreatedUser = await _userManager.FindByEmailAsync("");

                string subject = "Registeration Requests";
                string description = $"<h3>Hello Administrator </h3>/n" +
                    "Hello Administrator" +
                    "We noticed there are series of request which await acceptance, this scenario can either be" +
                    "the request count is more than 10 or a request was sent 2 hours ago ";


                await emailService.SendEmail(getCreatedUser.Email, subject, description);

                await Task.CompletedTask;
            }
            else
            {
                await Task.CompletedTask;
            }

        }
    }
}
