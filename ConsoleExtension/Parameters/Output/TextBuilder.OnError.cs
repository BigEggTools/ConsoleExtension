namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    internal partial class TextBuilder
    {
        private static IDictionary<ErrorType, Func<IEnumerable<Error>, int, string>> errorHandle = new Dictionary<ErrorType, Func<IEnumerable<Error>, int, string>>();

        private static void InitErrorHandle()
        {
            errorHandle.Add(ErrorType.DuplicateProperty, BuildDuplicatePropertyText);
            errorHandle.Add(ErrorType.VersionRequest, BuildVersionText);
            errorHandle.Add(ErrorType.HelpRequest, BuildHelpText);
        }


        private static string OnError(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            foreach (var pair in errorHandle)
            {
                if (errors.Any(e => e.ErrorType == pair.Key)) { return pair.Value(errors, maximumDisplayWidth); }
            }
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

        private static string BuildVersionText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            return OutputFormat.VERSION_INFO.Format(
                ProgramInfo.Default.Title,
                ProgramInfo.Default.Version,
                ProgramInfo.Default.Copyright,
                ProgramInfo.Default.Product,
                maximumDisplayWidth
            );
        }

        private static string BuildHelpText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BuildHeader(maximumDisplayWidth));

            return stringBuilder.ToString();
        }
    }
}
