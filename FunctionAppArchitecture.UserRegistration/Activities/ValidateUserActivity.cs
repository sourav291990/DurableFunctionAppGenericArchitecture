using FunctionAppArchitecture.Shared.Constants;
using FunctionAppArchitecture.Shared.Models;
using FunctionAppArchitecture.UserRegistration.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading.Tasks;

namespace FunctionAppArchitecture.UserRegistration.Activities
{
    public class ValidateUserActivity
    {
        private readonly IValidationService _validationService;
        public ValidateUserActivity(IValidationService validationService)
        {
            _validationService = validationService;
        }

        [FunctionName(StringConstants.ValidateUserActivity)]
        public async Task<ActivityResponse> RunAsync([ActivityTrigger] User user)
        {
            return await Task.Run(() => _validationService.Validate(user));
        }
    }
}
