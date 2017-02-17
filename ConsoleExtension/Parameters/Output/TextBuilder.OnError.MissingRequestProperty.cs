namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal partial class TextBuilder
    {
        private string BuildMissingRequestPropertyText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var error = errors.Single(err => err.ErrorType == ErrorType.MissingRequestProperty) as MissingRequestPropertyError;

            var messages = error.PropertyNames
                                .Select(name => $"Request property '{name}' in command '{error.CommandName}'.");

            return BuildString(new List<string>()
            {
                ApplicationHeaderText,
                ErrorHeaderText(errors),
                string.Join(Environment.NewLine, messages),
                $"For help on command detail, please run {error.CommandName} --help to check"
            }, maximumDisplayWidth);
        }
    }
}
