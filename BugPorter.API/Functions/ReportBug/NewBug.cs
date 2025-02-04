namespace BugPorter.API.Functions.ReportBug
{
    public class NewBug
    {
        public NewBug(string summary, string description)
        {
            Summary = summary;
            Description = description;
        }

        public string Summary { get; }
        public string Description { get;}
    }
}
