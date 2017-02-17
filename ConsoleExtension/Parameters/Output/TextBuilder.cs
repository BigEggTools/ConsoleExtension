namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Text;

    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [Export(typeof(ITextBuilder))]
    internal partial class TextBuilder : ITextBuilder
    {
        private readonly IProgramInfo programInfo;


        [ImportingConstructor]
        public TextBuilder(IProgramInfo programInfo)
        {
            this.programInfo = programInfo;

            Initialize();
        }


        public string Build(ParserResult result, int maximumDisplayWidth = Constants.DEFAULT_MAX_CONSOLE_LENGTH)
        {
            switch (result.ResultType)
            {
                case ParserResultType.ParseFailed:
                    return OnError(((ParseFailedResult)result).Errors, maximumDisplayWidth);
                case ParserResultType.ParseSuccess:
                    return OnSuccess((ParseSuccessResult)result, maximumDisplayWidth);
                default:
                    return string.Empty;
            }
        }


        private void Initialize()
        {
            InitErrorHandle();
        }

        private string BuildString(List<string> lines, int maximumDisplayWidth)
        {
            var stringBuilder = new StringBuilder();
            lines.ForEach(line => stringBuilder.AppendLine(line.FormatWithIndex(maximumDisplayWidth)));
            return stringBuilder.ToString();
        }


        private string ApplicationHeaderText { get { return $"{programInfo.Title}: v{programInfo.Version}"; } }
    }
}
