namespace BigEgg.Tools.ConsoleExtension.Parameters.Utils
{
    internal interface IProgramInfo
    {
        string Copyright { get; }
        string Product { get; }
        string Title { get; }
        string Version { get; }
    }
}