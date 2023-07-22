using FunctionAppArchitecture.Shared.Constants;
using FunctionAppArchitecture.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppArchitecture.UserRegistration.Services
{
    public class EmailService : IEmailService
    {
        public ActivityResponse SendEmail(Email email)
        {
            return new ActivityResponse { IsSuccess = true, ActivityStatus = Shared.Enums.ActivityStatus.Succeeded, ActivityName = StringConstants.SendEmailActivity };
        }
    }
}
