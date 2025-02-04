using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BugPorter.API.Functions.ReportBug.GitHub;
using BugPorter.API.Functions.ReportBug;

namespace BugPorter.API.Functions
{
    public class ReportBugFunction
    {
        private readonly CreateGitHubIssueCommand _createGitHubIssueCommand;
        private readonly ILogger<ReportBugFunction> _logger;

        public ReportBugFunction(CreateGitHubIssueCommand createGitHubIssueCommand, ILogger<ReportBugFunction> logger)
        {
            _createGitHubIssueCommand = createGitHubIssueCommand;
            _logger = logger;
        }

        [FunctionName("ReportBugFUnction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "bugs")] ReportBugRequest request)
        {
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
