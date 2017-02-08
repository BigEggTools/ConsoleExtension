namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using BigEgg.Tools.ConsoleExtension.Parameters.Results;

    internal interface ITextBuilder
    {
        string Build(ParserResult result, int maximumDisplayWidth = 80);
    }
}