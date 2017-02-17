namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    internal partial class TextBuilder
    {
        private string BuildCommandHelpRequestText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var error = errors.Single(err => err.ErrorType == ErrorType.CommandHelpRequest) as CommandHelpRequestError;

            var usage = error.CommandAttribute.Name + " "
                + string.Join(" ",
                              error.PropertyAttributes
                                   .Select(pa =>
                                   {
                                       return (pa.Required ? "" : "[") +
                                              $"--{pa.LongName} <{pa.PropertyAttributeType.ToString()}>" +
                                              (pa.Required ? "" : "]");
                                   }));
            var propertyInfos = error.PropertyAttributes
                                     .Select(pa => new Tuple<string, string>(
                                         $"--{pa.LongName} | --{pa.ShortName}",
                                         pa.HelpMessage));
            int propertyNameLenght = (int)(Math.Ceiling(propertyInfos.Max(message => message.Item1.Length) / ParameterConstants.TAB_LENGTH) * ParameterConstants.TAB_LENGTH);
            var propertyHelpMessages = propertyInfos.Select(message => $"    {message.Item1.FillWithCharacter(propertyNameLenght, ' ')}  {ParameterConstants.INDEX_START_STRING}{message.Item2}");

            return BuildString(new List<string>()
            {
                ApplicationHeaderText,
                $"Command '{error.CommandAttribute.Name}' - {ParameterConstants.INDEX_START_STRING}{error.CommandAttribute.HelpMessage}",
                "Usage:",
                usage,
                "Description:",
                string.Join(Environment.NewLine, propertyHelpMessages)
            }, maximumDisplayWidth);
        }
    }
}
