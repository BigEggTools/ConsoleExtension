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
            var error = errors.Single(e => e.ErrorType == ErrorType.HelpRequest) as HelpRequestError;

            var messages = new List<string>()
            {
                ApplicationHeaderText
            };
            messages.AddRange(BuildCommandHelpText(error.CommandAttributes));

            return BuildString(messages, maximumDisplayWidth);
        }


        private IEnumerable<string> BuildCommandHelpText(IEnumerable<CommandAttribute> attributes)
        {
            var introductionMessage = attributes.Count() > 1
                ? "Application support these commands:"
                : "Application support this command:";

            int commandLenght = (int)(Math.Ceiling(attributes.Max(attribute => attribute.Name.Length) / ParameterConstants.TAB_LENGTH) * ParameterConstants.TAB_LENGTH);
            var commandMessages = attributes.Select(attribute => $"    {attribute.Name.FillWithCharacter(commandLenght, ' ')} |  {ParameterConstants.INDEX_START_STRING}{attribute.HelpMessage}");

            return new List<string>()
            {
                introductionMessage,
                string.Join(Environment.NewLine, commandMessages),
                "For help on command detail, please run <command> --help to check"
            };
        }
    }
}
