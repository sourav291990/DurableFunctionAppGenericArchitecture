using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
using System;
using FunctionAppArchitecture.Shared.Models;
using FunctionAppArchitecture.Shared.Constants;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Net;

namespace FunctionAppArchitecture.UserRegistration.Starters
{
    public class RegisterUserStarter
    {
        [FunctionName(nameof(HttpStart))]
        public static async Task<IActionResult> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger logger)
        {
            logger.LogInformation("Processing the HttpRequest body");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            if (string.IsNullOrEmpty(requestBody))
            {
                return new BadRequestObjectResult("The body is missing!");
            }

            var appUser = JsonConvert.DeserializeObject<User>(requestBody);

            if (appUser == null)
            {
                return new BadRequestObjectResult("Invalid payload! Cannot be converted to the model.");
            }

            // Call the Orchestrator passing in the appUser as input
            var instanceId = await starter.StartNewAsync(StringConstants.RegisterUserOrchestrator, null, appUser);

            // Wait until the process is complete or timeout after 30 seconds so we can notify the caller(client)
            var response = await starter.WaitForCompletionOrCreateCheckStatusResponseAsync(req, instanceId, TimeSpan.FromSeconds(30));

            // Get the status of the process
            var status = await starter.GetStatusAsync(instanceId);
            // If it timed out
            if (status.RuntimeStatus != OrchestrationRuntimeStatus.Completed)
            {
                await starter.TerminateAsync(instanceId, "Timeout! Process took longer than expected!");

                return new ContentResult()
                {
                    Content = "{ Error: \"Timeout! Process took longer than expected!\"\nPlease try again! }",
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

            return response;

        }
    }
}
