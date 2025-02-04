using BugPorter.API.Functions.ReportBug.GitHub;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(BugPorter.API.Startup))]

namespace BugPorter.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<CreateGitHubIssueCommand>();
        }
    }
}
