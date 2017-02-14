namespace BigEgg.Tools.ConsoleExtension.IntegrationTests.Parameters.Params
{
    using BigEgg.Tools.ConsoleExtension.Parameters;

    [Command("", "Clone a repository into a new directory")]
    public class InvalidCommandParam
    {
        [StringProperty("repository", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.", Required = true)]
        public string Repository { get; set; }

        [StringProperty("rep", "b", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.", Required = true)]
        public string Branch { get; set; }

        [StringProperty("rep", "b", "Some data.")]
        private string Data { get; set; }

        [StringProperty("data2", "d2", "Another data.")]
        public string Data2 { get; }
    }
}
