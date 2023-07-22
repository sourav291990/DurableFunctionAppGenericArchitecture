using FunctionAppArchitecture.Shared.Constants;
using FunctionAppArchitecture.Shared.Models;
using FunctionAppArchitecture.UserRegistration.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading.Tasks;

namespace FunctionAppArchitecture.UserRegistration.Activities
{
    public class SaveUserActivity
    {
        private readonly IUserService _userService;
        public SaveUserActivity(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName(StringConstants.CreateUserActivity)]
        public async Task<ActivityResponse> RunAsync([ActivityTrigger] User user)
        {
            return await Task.Run(() => _userService.CreateUser(user));
        }
    }
}
