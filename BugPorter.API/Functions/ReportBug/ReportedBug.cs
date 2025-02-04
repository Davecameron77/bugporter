namespace BugPorter.API.Functions.ReportBug
{
    public class ReportedBug
    {
        public ReportedBug(string id, string summary, string description)
        {
            Id = id;
            Summary = summary;
            Description = description;
        }

        public string Id { get; }
        public string Summary { get; }
        public string Description { get; }
    }
}
