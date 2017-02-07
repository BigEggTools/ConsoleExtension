namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    internal class TextBuilder
    {
        internal static string Build(ParserResult result, int maximumDisplayWidth = Constants.DEFAULT_MAX_CONSOLE_LENGTH)
        {
            if (result.ResultType == ParserResultType.ParseFailed)
            {
                return BuildHelp(((ParseFailedResult)result).Errors, maximumDisplayWidth);
            }
            return string.Empty;
        }


        private static string BuildHelp(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            if (errors.Any(e => e.ErrorType == ErrorType.DuplicateProperty)) { return BuildDuplicatePropertyText(errors, maximumDisplayWidth); }
            if (errors.Any(e => e.ErrorType == ErrorType.VersionRequest)) { return BuildVersionText(maximumDisplayWidth); }
            if (errors.Any(e => e.ErrorType == ErrorType.HelpRequest)) { return BuildHelpText(maximumDisplayWidth); }
            throw new NotImplementedException();
        }


        private static string BuildDuplicatePropertyText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var error = errors.First(e => e.ErrorType == ErrorType.DuplicateProperty) as DuplicatePropertyError;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BuildHeader(maximumDisplayWidth));
            stringBuilder.AppendLine(OutputFormat.ERROR_HEADER.Format(maximumDisplayWidth));
            stringBuilder.AppendLine(OutputFormat.DUPLICATE_PROPERTY.Format(
                error.PropertyName,
                maximumDisplayWidth
            ));

            return stringBuilder.ToString();
        }

        private static string BuildVersionText(int maximumDisplayWidth)
        {
            return OutputFormat.VERSION_INFO.Format(
                ProgramInfo.Default.Title,
                ProgramInfo.Default.Version,
                ProgramInfo.Default.Copyright,
                ProgramInfo.Default.Product,
                maximumDisplayWidth
            );
        }

        private static string BuildHeader(int maximumDisplayWidth)
        {
            return OutputFormat.HEADER.Format(
                ProgramInfo.Default.Title,
                ProgramInfo.Default.Version,
                maximumDisplayWidth
            );
        }

        private static string BuildHelpText(int maximumDisplayWidth)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BuildHeader(maximumDisplayWidth));

            return stringBuilder.ToString();
        }
    }
}
