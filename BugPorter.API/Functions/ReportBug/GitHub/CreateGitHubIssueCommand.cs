using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octokit;
using System.Threading.Tasks;

namespace BugPorter.API.Functions.ReportBug.GitHub
{
    public class CreateGitHubIssueCommand
    {
        private readonly GitHubClient _gitHubClient;
        private readonly GitHubRepositoryOptions _gitHubRepositoryOptions;
        private readonly ILogger<CreateGitHubIssueCommand> _logger;

        public CreateGitHubIssueCommand(ILogger<CreateGitHubIssueCommand> logger, GitHubClient gitHubClient, IOptions<GitHubRepositoryOptions> gitHubRepositoryOptions)
        {
            _logger = logger;
            _gitHubClient = gitHubClient;
            _gitHubRepositoryOptions = gitHubRepositoryOptions.Value;
        }

        public async Task<ReportedBug> Execute(NewBug newBug)
        {
            _logger.LogInformation("Creating GitHub issue");

            // Do stuff
            NewIssue newIssue = new NewIssue(newBug.Summary)
            {
                Body = newBug.Description
            };
            Issue createdIssue = await _gitHubClient.Issue.Create(_gitHubRepositoryOptions.Owner, _gitHubRepositoryOptions.Name, newIssue);

            _logger.LogInformation($"GitHub issue created with Id {createdIssue.Number}");

            return new ReportedBug(createdIssue.Number.ToString(), createdIssue.Title, createdIssue.Body); ;
        }
    }
}
