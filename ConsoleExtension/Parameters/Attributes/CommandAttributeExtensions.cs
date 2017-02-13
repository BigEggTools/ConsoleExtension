namespace BigEgg.Tools.ConsoleExtension.Parameters
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal static class CommandAttributeExtensions
    {
        private static Regex COMMAND_NAME_VALIDATE_REGEX = new Regex("^[a-zA-Z0-9-]{1,16}$");
        private static Regex COMMAND_HELP_MESSAGE_VALIDATE_REGEX = new Regex("^.{1,128}$");


        public static IList<Error> Validate(this CommandAttribute attribute, string typeName)
        {
            var result = new List<Error>();

            if (attribute == null)
            {
                result.Add(new DevelopMissingCommandError(typeName));
                return result;
            }

            var error = ValidateProperty(attribute.Name, typeName, "Name", 16, COMMAND_NAME_VALIDATE_REGEX);
            if (error != null) { result.Add(error); }
            error = ValidateProperty(attribute.HelpMessage, typeName, "HelpMessage", 128, COMMAND_HELP_MESSAGE_VALIDATE_REGEX);
            if (error != null) { result.Add(error); }

            return result;
        }

        private static Error ValidateProperty(string value, string typeName, string propertyName, int maxLength, Regex regex)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new DevelopInvalidCommandError(typeName, propertyName, InvalidType.Empty);
            }
            else
            {
                if (value.Length > maxLength)
                {
                    return new DevelopInvalidCommandError(typeName, propertyName, InvalidType.TooLong);
                }
                if (!regex.IsMatch(value))
                {
                    return new DevelopInvalidCommandError(typeName, propertyName, InvalidType.RegexInvalid, regex.ToString());
                }
            }

            return null;
        }
    }
}
