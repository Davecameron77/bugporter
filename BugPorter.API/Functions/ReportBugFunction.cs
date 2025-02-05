using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BugPorter.API.Functions.ReportBug.GitHub;
using BugPorter.API.Functions.ReportBug;
using FirebaseAdminAuthentication.DependencyInjection.Services;
using Microsoft.AspNetCore.Authentication;
using FirebaseAdminAuthentication.DependencyInjection.Models;

namespace BugPorter.API.Functions
{
    public class ReportBugFunction
    {
        private readonly CreateGitHubIssueCommand _createGitHubIssueCommand;
        private readonly FirebaseAuthenticationFunctionHandler _authenticationHandler;
        private readonly ILogger<ReportBugFunction> _logger;

        public ReportBugFunction(CreateGitHubIssueCommand createGitHubIssueCommand, ILogger<ReportBugFunction> logger, FirebaseAuthenticationFunctionHandler authenticationHandler)
        {
            _createGitHubIssueCommand = createGitHubIssueCommand;
            _logger = logger;
            _authenticationHandler = authenticationHandler;
        }

        [FunctionName("ReportBugFUnction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "bugs")] ReportBugRequest request, HttpRequest httpRequest)
        {
            AuthenticateResult authenticationResult = await _authenticationHandler.HandleAuthenticateAsync(httpRequest);

            if (!authenticationResult.Succeeded)
            {
                return new UnauthorizedResult();
            }

            string userId = authenticationResult.Principal.FindFirst(FirebaseUserClaimType.ID).Value;
            _logger.LogInformation($"User {userId} is reporting a bug");

            NewBug newBug = new NewBug(request.Summary, request.Description);
            ReportedBug reportedBug = await _createGitHubIssueCommand.Execute(newBug);

            return new OkObjectResult(new ReportBugResponse()
            {
                Id = reportedBug.Id,
                Summary = reportedBug.Summary,
                Description = reportedBug.Description
            });
        }
    }
}
