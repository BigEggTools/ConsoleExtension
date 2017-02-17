namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.FakeParameters
{
    using BigEgg.Tools.ConsoleExtension.Parameters;

    [Command("Clone", "Clone a repository into a new directory")]
    public class GitClone
    {
        [StringProperty("repository", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.", Required = true)]
        public string Repository { get; set; }

        [BooleanProperty("Recurse", "r", "If new commits of all populated submodules should be fetched too.")]
        public bool Recurse { get; set; }
    }
}
