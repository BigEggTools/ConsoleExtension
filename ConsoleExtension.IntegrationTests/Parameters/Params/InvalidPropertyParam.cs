namespace BigEgg.Tools.ConsoleExtension.IntegrationTests.Parameters.Params
{
    using BigEgg.Tools.ConsoleExtension.Parameters;

    [Command("Pull", "Fetch from and integrate with another repository or a local branch")]
    public class InvalidPropertyParam
    {
        [StringProperty("repository", "rep", "The \"remote\" repository that is the source of a fetch or pull operation.", Required = true)]
        public string Repository { get; set; }

        [StringProperty("rep", "b", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.", Required = true)]
        public string Branch { get; set; }

        [StringProperty("rep", "b", "Some data.")]
        private string Data { get; set; }

        [StringProperty("data2", "d2", "Another data.")]
        public string Data2 { get; }
    }
}
