namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal partial class TextBuilder
    {
        private string BuildEmptyInputText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            return BuildString(new List<string>()
            {
                ApplicationHeaderText,
                ErrorHeaderText(errors),
                $"No input, please use '--help' see the help document."
            }, maximumDisplayWidth);
        }
    }
}
