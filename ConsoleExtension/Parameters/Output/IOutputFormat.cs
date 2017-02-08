using BigEgg.Tools.ConsoleExtension.Parameters.Output.Text;

namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    internal interface IOutputFormat
    {
        ApplicationHeader APPLICATION_HEADER { get; }
        DuplicateProperty DUPLICATE_PROPERTY { get; }
        ErrorHeader ERROR_HEADER { get; }
        VersionInfo VERSION_INFO { get; }
        EmptyInput EMPTY_INPUT { get; }
    }
}