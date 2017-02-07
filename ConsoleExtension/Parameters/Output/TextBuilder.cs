namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    internal partial class TextBuilder
    {
        static TextBuilder()
        {
            InitErrorHandle();
        }


        internal static string Build(ParserResult result, int maximumDisplayWidth = Constants.DEFAULT_MAX_CONSOLE_LENGTH)
        {
            if (result.ResultType == ParserResultType.ParseFailed)
            {
                return OnError(((ParseFailedResult)result).Errors, maximumDisplayWidth);
            }
            return string.Empty;
        }


        private static string BuildHeader(int maximumDisplayWidth)
        {
            return OutputFormat.APPLICATION_HEADER.Format(
                ProgramInfo.Default.Title,
                ProgramInfo.Default.Version,
                maximumDisplayWidth
            );
        }
    }
}
