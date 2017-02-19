namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    internal partial class TextBuilder
    {
        private string BuildMissingCommandText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var error = errors.Single(err => err.ErrorType == ErrorType.MissingCommand) as MissingCommandError;

            var messages = new List<string>()
            {
                ApplicationHeaderText,
                ErrorHeaderText(errors),
                "Not found command. Please specific the command you'd like to execute.",
            };
            messages.AddRange(BuildCommandHelpText(error.CommandAttributes));

            return BuildString(messages, maximumDisplayWidth);
        }

        private string BuildUnknownCommandText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var error = errors.Single(err => err.ErrorType == ErrorType.UnknownCommand) as UnknownCommandError;

            var messages = new List<string>()
            {
                ApplicationHeaderText,
                ErrorHeaderText(errors),
                $"Unknown command '{error.CommandName}' found. Please specific the command you'd like to execute.",
            };
            messages.AddRange(BuildCommandHelpText(error.CommandAttributes));

            return BuildString(messages, maximumDisplayWidth);
        }
    }
}
