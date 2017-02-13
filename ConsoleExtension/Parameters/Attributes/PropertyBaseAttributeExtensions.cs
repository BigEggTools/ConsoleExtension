namespace BigEgg.Tools.ConsoleExtension.Parameters
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal static class PropertyBaseAttributeExtensions
    {
        private static readonly Regex PROPERTY_LONG_NAME_VALIDATE_REGEX = new Regex("^[a-zA-Z0-9_]{1,16}$");
        private static readonly Regex PROPERTY_SHORT_NAME_VALIDATE_REGEX = new Regex("^[a-zA-Z0-9_]{1,3}$");
        private static readonly Regex PROPERTY_HELP_MESSAGE_VALIDATE_REGEX = new Regex("^.{1,128}$");
        private static readonly IDictionary<PropertyAttributeType, Func<string, _PropertyInfo, DevelopPropertyTypeMismatchError>> validateTypeMatch;

        static PropertyBaseAttributeExtensions()
        {
            validateTypeMatch = new Dictionary<PropertyAttributeType, Func<string, _PropertyInfo, DevelopPropertyTypeMismatchError>>();
            validateTypeMatch.Add(PropertyAttributeType.String, ValidateStringPropertyAttributeTypeMatch);
        }

        public static IList<Error> Validate(this PropertyBaseAttribute attribute, string typeName, _PropertyInfo propertyInfo)
        {
            var result = new List<Error>();

            var error = ValidateProperty(attribute.LongName, typeName, propertyInfo, "LongName", 16, PROPERTY_LONG_NAME_VALIDATE_REGEX);
            if (error != null) { result.Add(error); }
            error = ValidateProperty(attribute.ShortName, typeName, propertyInfo, "ShortName", 3, PROPERTY_SHORT_NAME_VALIDATE_REGEX);
            if (error != null) { result.Add(error); }
            error = ValidateProperty(attribute.HelpMessage, typeName, propertyInfo, "HelpMessage", 128, PROPERTY_HELP_MESSAGE_VALIDATE_REGEX);
            if (error != null) { result.Add(error); }

            var typeMatchError = validateTypeMatch[attribute.PropertyAttributeType](typeName, propertyInfo);
            if (typeMatchError != null) { result.Add(typeMatchError); }

            if (!propertyInfo.CanWrite) { result.Add(new DevelopPropertyCannotWriteError(typeName, propertyInfo.Name)); }

            return result;
        }

        private static Error ValidateProperty(string value, string typeName, _PropertyInfo propertyInfo, string propertyName, int maxLength, Regex regex)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new DevelopInvalidPropertyError(typeName, propertyInfo.Name, propertyName, InvalidType.Empty);
            }
            else
            {
                if (value.Length > maxLength)
                {
                    return new DevelopInvalidPropertyError(typeName, propertyInfo.Name, propertyName, InvalidType.TooLong);
                }
                if (!regex.IsMatch(value))
                {
                    return new DevelopInvalidPropertyError(typeName, propertyInfo.Name, propertyName, InvalidType.RegexInvalid, regex.ToString());
                }
            }

            return null;
        }

        private static DevelopPropertyTypeMismatchError ValidateStringPropertyAttributeTypeMatch(string typeName, _PropertyInfo propertyInfo)
        {
            var supportedTypes = new List<string>()
            {
                typeof(string).Name
            };
            if (supportedTypes.Contains(propertyInfo.PropertyType.Name)) { return null; }

            return new DevelopPropertyTypeMismatchError(typeName, propertyInfo.Name, propertyInfo.PropertyType.Name, supportedTypes);
        }
    }
}
