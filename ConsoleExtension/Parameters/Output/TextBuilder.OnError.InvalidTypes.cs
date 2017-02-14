﻿namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System.Collections.Generic;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal partial class TextBuilder
    {
        private string BuildInvalidTypesText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            var errorMessages = new List<string>()
            {
                ApplicationHeaderText,
                ErrorHeaderText(errors),
            };

            foreach (var error in errors)
            {
                switch (error.ErrorType)
                {
                    case ErrorType.Develop_DuplicateCommand:
                        errorMessages.Add(BuildDevelopDuplicateCommandText(error));
                        break;
                    case ErrorType.Develop_DuplicateProperty:
                        errorMessages.Add(BuildDevelopDuplicatePropertyText(error));
                        break;
                    case ErrorType.Develop_InvalidCommand:
                        errorMessages.Add(BuildDevelopInvalidCommandText(error));
                        break;
                    case ErrorType.Develop_InvalidProperty:
                        errorMessages.Add(BuildDevelopInvalidPropertyText(error));
                        break;
                    case ErrorType.Develop_MissingCommand:
                        errorMessages.Add(BuildDevelopMissingCommandText(error));
                        break;
                    case ErrorType.Develop_PropertyTypeCannotWrite:
                        errorMessages.Add(BuildDevelopPropertyTypeCannotWriteText(error));
                        break;
                    case ErrorType.Develop_PropertyTypeMismatch:
                        errorMessages.Add(BuildDevelopPropertyTypeMismatchText(error));
                        break;
                    default:
                        break;
                }
            }

            return BuildString(errorMessages, maximumDisplayWidth);
        }

        private string BuildDevelopDuplicateCommandText(Error error)
        {
            var err = error as DevelopDuplicateCommandError;
            return $"Command name '{err.CommandName}' had been declare in both type '{err.TypeName1}' and '{err.TypeName2}'.";
        }

        private string BuildDevelopDuplicatePropertyText(Error error)
        {
            var err = error as DevelopDuplicatePropertyError;
            return $"Property name '{err.AttributeName}' in type '{err.TypeName}' had been declare in both property '{err.PropertyName1}' and '{err.PropertyName2}'.";
        }

        private string BuildDevelopInvalidCommandText(Error error)
        {
            var err = error as DevelopInvalidCommandError;
            switch (err.InvalidType)
            {
                case InvalidType.Empty:
                    return $"Parameter '{err.PropertyName}' of Command in '{err.TypeName}' should not be null, empty or white spaces.";
                case InvalidType.TooLong:
                    return $"Parameter '{err.PropertyName}' of Command in '{err.TypeName}' is too long.";
                case InvalidType.RegexInvalid:
                    return $"Parameter '{err.PropertyName}' of Command in '{err.TypeName}' should match regex {err.Regex}.";
            }

            return string.Empty;
        }

        private string BuildDevelopInvalidPropertyText(Error error)
        {
            var err = error as DevelopInvalidPropertyError;
            switch (err.InvalidType)
            {
                case InvalidType.Empty:
                    return $"Parameter '{err.AttributePropertyName}' of Property '{err.PropertyName}' in '{err.TypeName}' should not be null, empty or white spaces.";
                case InvalidType.TooLong:
                    return $"Parameter '{err.AttributePropertyName}' of Property '{err.PropertyName}' in '{err.TypeName}' is too long.";
                case InvalidType.RegexInvalid:
                    return $"Parameter '{err.AttributePropertyName}' of Property '{err.PropertyName}' in '{err.TypeName}' should match regex {err.Regex}.";
            }

            return string.Empty;
        }

        private string BuildDevelopMissingCommandText(Error error)
        {
            var err = error as DevelopMissingCommandError;
            return $"Cannot find CommandAttribute on type '{err.TypeName}'.";
        }

        private string BuildDevelopPropertyTypeCannotWriteText(Error error)
        {
            var err = error as DevelopPropertyCannotWriteError;
            return $"Property '{err.PropertyName}' in type '{err.TypeName}' had been writen.";
        }

        private string BuildDevelopPropertyTypeMismatchText(Error error)
        {
            var err = error as DevelopPropertyTypeMismatchError;
            return $"Property '{err.PropertyName}' in type '{err.TypeName}' currently is '{err.CurrentType}', that attribute only support [ {string.Join(", ", err.SupportedTypes)} ].";
        }
    }
}
