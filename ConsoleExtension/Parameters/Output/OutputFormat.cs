namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System.ComponentModel.Composition;

    using BigEgg.Tools.ConsoleExtension.Parameters.Output.Text;

    [Export(typeof(IOutputFormat))]
    internal class OutputFormat : IOutputFormat
    {
        public OutputFormat()
        {
            VERSION_INFO = new VersionInfo();
            APPLICATION_HEADER = new ApplicationHeader();
            ERROR_HEADER = new ErrorHeader();
            DUPLICATE_PROPERTY = new DuplicateProperty();
            EMPTY_INPUT = new EmptyInput();
        }

        public VersionInfo VERSION_INFO { get; private set; }
        public ApplicationHeader APPLICATION_HEADER { get; private set; }
        public ErrorHeader ERROR_HEADER { get; private set; }
        public DuplicateProperty DUPLICATE_PROPERTY { get; private set; }
        public EmptyInput EMPTY_INPUT { get; private set; }
    }
}
