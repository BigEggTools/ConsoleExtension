namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal partial class TextBuilder
    {
        private IDictionary<ErrorType, Func<IEnumerable<Error>, int, string>> errorHandle = new Dictionary<ErrorType, Func<IEnumerable<Error>, int, string>>();

        private void InitErrorHandle()
        {
            errorHandle.Add(ErrorType.EmptyInput, BuildEmptyInputText);
            errorHandle.Add(ErrorType.DuplicateProperty, BuildDuplicatePropertyText);
            errorHandle.Add(ErrorType.VersionRequest, BuildVersionText);
            errorHandle.Add(ErrorType.HelpRequest, BuildHelpText);
        }


        private string OnError(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            foreach (var pair in errorHandle)
            {
                if (errors.Any(e => e.ErrorType == pair.Key)) { return pair.Value(errors, maximumDisplayWidth); }
            }
            throw new NotImplementedException();
        }


        private string BuildEmptyInputText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var error = errors.First(e => e.ErrorType == ErrorType.EmptyInput) as DuplicatePropertyError;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BuildHeader(maximumDisplayWidth));
            stringBuilder.AppendLine(outputFormat.ERROR_HEADER.Format(maximumDisplayWidth));
            stringBuilder.AppendLine(outputFormat.EMPTY_INPUT.Format(maximumDisplayWidth));

            return stringBuilder.ToString();
        }

        private string BuildDuplicatePropertyText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var error = errors.First(e => e.ErrorType == ErrorType.DuplicateProperty) as DuplicatePropertyError;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BuildHeader(maximumDisplayWidth));
            stringBuilder.AppendLine(outputFormat.ERROR_HEADER.Format(maximumDisplayWidth));
            stringBuilder.AppendLine(outputFormat.DUPLICATE_PROPERTY.Format(
                error.PropertyName,
                maximumDisplayWidth
            ));

            return stringBuilder.ToString();
        }

        private string BuildVersionText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            return outputFormat.VERSION_INFO.Format(
                programInfo.Title,
                programInfo.Version,
                programInfo.Copyright,
                programInfo.Product,
                maximumDisplayWidth
            );
        }

        private string BuildHelpText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BuildHeader(maximumDisplayWidth));

            return stringBuilder.ToString();
        }
    }
}
