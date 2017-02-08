namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;
    using System.ComponentModel.Composition;

    [Export(typeof(ITextBuilder))]
    internal partial class TextBuilder : ITextBuilder
    {
        private readonly IProgramInfo programInfo;
        private readonly IOutputFormat outputFormat;


        [ImportingConstructor]
        public TextBuilder(IProgramInfo programInfo, IOutputFormat outputFormat)
        {
            this.programInfo = programInfo;
            this.outputFormat = outputFormat;

            Initialize();
        }


        public string Build(ParserResult result, int maximumDisplayWidth = Constants.DEFAULT_MAX_CONSOLE_LENGTH)
        {
            if (result.ResultType == ParserResultType.ParseFailed)
            {
                return OnError(((ParseFailedResult)result).Errors, maximumDisplayWidth);
            }
            return string.Empty;
        }


        private void Initialize()
        {
            InitErrorHandle();
        }

        private string BuildHeader(int maximumDisplayWidth)
        {
            return outputFormat.APPLICATION_HEADER.Format(
                programInfo.Title,
                programInfo.Version,
                maximumDisplayWidth
            );
        }
    }
}
