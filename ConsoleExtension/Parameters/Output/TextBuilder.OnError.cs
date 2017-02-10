namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal partial class TextBuilder
    {
        private IDictionary<ErrorType, Func<IEnumerable<Error>, int, string>> errorHandle = new Dictionary<ErrorType, Func<IEnumerable<Error>, int, string>>();

        private void InitErrorHandle()
        {
            errorHandle.Add(ErrorType.EmptyInput, BuildEmptyInputText);


            errorHandle.Add(ErrorType.DuplicateProperty, BuildDuplicatePropertyText);
            errorHandle.Add(ErrorType.VersionRequest, BuildVersionText);
        }

        private string OnError(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            foreach (var pair in errorHandle)
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
