namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    internal partial class TextBuilder
    {
        private string BuildMissingCommandText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var missingCommandError = errors.FirstOrDefault(error => error.ErrorType == ErrorType.MissingCommand) as MissingCommandError;

            var introductionMessage = missingCommandError.CommandAttributes.Count() > 1
                ? "Application support these commands:"
                : "Application support this command:";

            int commandLenght = (int)(Math.Ceiling(missingCommandError.CommandAttributes.Max(attribute => attribute.Name.Length) / ParameterConstants.TAB_LENGTH) * ParameterConstants.TAB_LENGTH);
            var commandMessages = missingCommandError.CommandAttributes
                                                     .Select(attribute => $"    {attribute.Name.FillWithCharacter(commandLenght, ' ')} | {ParameterConstants.INDEX_START_STRING} {attribute.HelpMessage}");
            return BuildString(new List<string>()
            {
                ApplicationHeaderText,
                ErrorHeaderText(errors),
                "Not found command. Please specific the command you'd like to execute.",
                introductionMessage,
                string.Join(Environment.NewLine, commandMessages),
                "For help on command detail, please run <command> --help to check"
            }, maximumDisplayWidth);
        }

        private string BuildUnknownCommandText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var unknownCommandError = errors.FirstOrDefault(error => error.ErrorType == ErrorType.UnknownCommand) as UnknownCommandError;

            var introductionMessage = unknownCommandError.CommandAttributes.Count() > 1
                ? "Application support these commands:"
                : "Application support this command:";

            int commandLenght = (int)(Math.Ceiling(unknownCommandError.CommandAttributes.Max(attribute => attribute.Name.Length) / ParameterConstants.TAB_LENGTH) * ParameterConstants.TAB_LENGTH);
            var commandMessages = unknownCommandError.CommandAttributes
                                                     .Select(attribute => $"    {attribute.Name.FillWithCharacter(commandLenght, ' ')} | {ParameterConstants.INDEX_START_STRING} {attribute.HelpMessage}");
            return BuildString(new List<string>()
            {
                ApplicationHeaderText,
                ErrorHeaderText(errors),
                $"Unknown command '{unknownCommandError.CommandName}' found. Please specific the command you'd like to execute.",
                introductionMessage,
                string.Join(Environment.NewLine, commandMessages),
                "For help on command detail, please run <command> --help to check"
            }, maximumDisplayWidth);
        }
    }
}
