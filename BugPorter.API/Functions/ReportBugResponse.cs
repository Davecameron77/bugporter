using System.Reflection.Metadata.Ecma335;

namespace BugPorter.API.Functions
{
    public class ReportBugResponse
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}
