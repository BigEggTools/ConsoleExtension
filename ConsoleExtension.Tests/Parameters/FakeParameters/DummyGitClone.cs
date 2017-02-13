namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.FakeParameters
{
    using BigEgg.Tools.ConsoleExtension.Parameters;

    [Command("", "Clone a repository into a new directory")]
    public class DummyGitClone
    {
        [StringProperty("repository", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.", Required = true)]
        public string Repository { get; set; }

        [StringProperty("branch", "b", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.", Required = true)]
        private string Branch { get; set; }
    }
}
