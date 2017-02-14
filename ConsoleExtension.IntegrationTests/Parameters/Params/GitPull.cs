namespace BigEgg.Tools.ConsoleExtension.IntegrationTests.Parameters.Params
{
    using BigEgg.Tools.ConsoleExtension.Parameters;

    [Command("Pull", "Fetch from and integrate with another repository or a local branch")]
    public class GitPull
    {
        [StringProperty("repository", "rep", "The \"remote\" repository that is the source of a fetch or pull operation.", Required = true)]
        public string Repository { get; set; }
    }
}
