namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal partial class TextBuilder
    {
        private string BuildDuplicatePropertyText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var error = errors.First(e => e.ErrorType == ErrorType.DuplicateProperty) as DuplicatePropertyError;

            return BuildString(new List<string>()
            {
                ApplicationHeaderText,
                ErrorHeaderText(errors),
                $"Argument '{error.PropertyName}' had been set multiple times."
            }, maximumDisplayWidth);
        }
    }
}
