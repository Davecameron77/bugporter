using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BugPorter.API.Functions.ReportBug.GitHub
{
    public class CreateGitHubIssueCommand
    {
        private readonly ILogger<CreateGitHubIssueCommand> _logger;

        public CreateGitHubIssueCommand(ILogger<CreateGitHubIssueCommand> logger)
        {
            _logger = logger;
        }

        public async Task<ReportedBug> Execute(NewBug newBug)
        {
            _logger.LogInformation("Creating GitHub issue");

            // Do stuff
            ReportedBug reportedBug = new ReportedBug("1", newBug.Summary, newBug.Description);

            _logger.LogInformation($"GitHub issue created with Id {reportedBug.Id}");

            return reportedBug;
        }
    }
}
