using FunctionAppArchitecture.Shared.Constants;
using FunctionAppArchitecture.Shared.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppArchitecture.UserRegistration.Orchestrators
{
    public class RegisterUserOrchestrator
    {
        [FunctionName(nameof(RegisterUser))]
        public async Task<object> RegisterUser([OrchestrationTrigger] IDurableOrchestrationContext context, ILogger logger)
        {
            logger = context.CreateReplaySafeLogger(logger);

            var appUser = context.GetInput<User>();

            ActivityResponse validateDataResponse = null;
            ActivityResponse persistDataResponse = null;
            ActivityResponse sendConfirmEmailResponse = null;

            try
            {
                logger.LogInformation("user validation starting...");
                validateDataResponse = await context.CallActivityAsync<ActivityResponse>(StringConstants.ValidateUserActivity, appUser);
                if (!validateDataResponse.IsSuccess)
                {
                    throw new Exception();
                }

                logger.LogInformation("user saving starting...");
                persistDataResponse = await context.CallActivityAsync<ActivityResponse>(StringConstants.CreateUserActivity, appUser);
                if (!persistDataResponse.IsSuccess)
                {
                    throw new Exception();
                }

                logger.LogInformation("sending email...");
                sendConfirmEmailResponse = await context.CallActivityAsync<ActivityResponse>(StringConstants.SendEmailActivity, appUser);

                if (!sendConfirmEmailResponse.IsSuccess)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new
                {
                    Summary = "Failed",
                    ValidateDataResponse = validateDataResponse,
                    PersistDataResponse = persistDataResponse,
                    SendConfirmEmailResponse = sendConfirmEmailResponse,
                };
            }

            return new
            {
                Summary = "Successful",
                ValidateDataResponse = validateDataResponse,
                PersistDataResponse = persistDataResponse,
                SendConfirmEmailResponse = sendConfirmEmailResponse,
            };
        }
    }
}
