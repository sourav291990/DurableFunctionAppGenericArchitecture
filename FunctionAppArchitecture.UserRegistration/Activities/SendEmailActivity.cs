using FunctionAppArchitecture.Shared.Constants;
using FunctionAppArchitecture.Shared.Models;
using FunctionAppArchitecture.UserRegistration.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading.Tasks;

namespace FunctionAppArchitecture.UserRegistration.Activities
{
    public class SendEmailActivity
    {
        private readonly IEmailService _emailService;
        public SendEmailActivity(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [FunctionName(StringConstants.SendEmailActivity)]
        public async Task<ActivityResponse> RunAsync([ActivityTrigger] Email email)
        {
            return await Task.Run(() => _emailService.SendEmail(email));
        }
    }
}
