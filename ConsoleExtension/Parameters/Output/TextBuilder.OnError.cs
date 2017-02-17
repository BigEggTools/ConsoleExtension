namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal partial class TextBuilder
    {
        private IDictionary<ErrorType, Func<IEnumerable<Error>, int, string>> errorHandles = new Dictionary<ErrorType, Func<IEnumerable<Error>, int, string>>();

        private void InitErrorHandle()
        {
            errorHandles.Add(ErrorType.EmptyInput, BuildEmptyInputText);
            errorHandles.Add(ErrorType.Develop_DuplicateCommand, BuildInvalidTypesText);
            errorHandles.Add(ErrorType.Develop_DuplicateProperty, BuildInvalidTypesText);
            errorHandles.Add(ErrorType.Develop_InvalidCommand, BuildInvalidTypesText);
            errorHandles.Add(ErrorType.Develop_InvalidProperty, BuildInvalidTypesText);
            errorHandles.Add(ErrorType.Develop_MissingCommand, BuildInvalidTypesText);
            errorHandles.Add(ErrorType.Develop_PropertyTypeCannotWrite, BuildInvalidTypesText);
            errorHandles.Add(ErrorType.Develop_PropertyTypeMismatch, BuildInvalidTypesText);
            errorHandles.Add(ErrorType.DuplicateArgument, BuildDuplicatePropertyText);
            errorHandles.Add(ErrorType.VersionRequest, BuildVersionText);
            errorHandles.Add(ErrorType.HelpRequest, BuildHelpRequestText);
            errorHandles.Add(ErrorType.MissingCommand, BuildMissingCommandText);
            errorHandles.Add(ErrorType.UnknownCommand, BuildUnknownCommandText);
            errorHandles.Add(ErrorType.CommandHelpRequest, BuildCommandHelpRequestText);
            errorHandles.Add(ErrorType.MissingRequestProperty, BuildMissingRequestPropertyText);

            InitInvalidTypeHandles();
        }

        private string OnError(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            foreach (var pair in errorHandles)
            {
                if (errors.Any(e => e.ErrorType == pair.Key)) { return pair.Value(errors, maximumDisplayWidth); }
            }
            throw new NotImplementedException();
        }

        private string ErrorHeaderText(IEnumerable<Error> errors)
        {
            var errorMessage = errors.Count() > 1 ? "Some errors" : "An error";
            return $"{errorMessage} occurs, please see the detail message:";
        }
    }
}
