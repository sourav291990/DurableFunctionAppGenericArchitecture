using FunctionAppArchitecture.Shared.Constants;
using FunctionAppArchitecture.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppArchitecture.UserRegistration.Services
{
    public class UserService : IUserService
    {
        public ActivityResponse CreateUser(User user)
        {
            return new ActivityResponse { IsSuccess = true, ActivityStatus = Shared.Enums.ActivityStatus.Succeeded, ActivityName = StringConstants.CreateUserActivity };
        }
    }
}
