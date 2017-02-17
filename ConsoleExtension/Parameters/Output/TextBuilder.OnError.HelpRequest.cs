namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    internal partial class TextBuilder
    {
        private string BuildHelpRequestText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var helpRequestError = errors.FirstOrDefault(error => error.ErrorType == ErrorType.HelpRequest) as HelpRequestError;

            var introductionMessage = helpRequestError.CommandAttributes.Count() > 1
                ? "Application support these commands:"
                : "Application support this command:";

            int commandLenght = (int)Math.Ceiling(helpRequestError.CommandAttributes.Max(attribute => attribute.Name.Length) / 4.0) * 4;
            var commandMessages = helpRequestError.CommandAttributes
                                                  .Select(attribute => $"    {attribute.Name.FillWithCharacter(commandLenght, ' ')} | {ParameterConstants.INDEX_START_STRING} {attribute.HelpMessage}");
            return BuildString(new List<string>()
            {
                ApplicationHeaderText,
                introductionMessage,
                string.Join(Environment.NewLine, commandMessages),
                "For help on command detail, please run <command> --help to check"
            }, maximumDisplayWidth);
        }
    }
}
