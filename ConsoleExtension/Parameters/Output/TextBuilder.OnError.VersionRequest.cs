namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal partial class TextBuilder
    {
        private string BuildVersionText(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            return string.Join(Environment.NewLine, new string[]
            {
                "Program version information:",
                $"Program Name: {ParameterConstants.INDEX_START_STRING}{programInfo.Title}",
                $"Program Version: {ParameterConstants.INDEX_START_STRING}{programInfo.Version}",
                $"Program Product: {ParameterConstants.INDEX_START_STRING}{programInfo.Copyright}",
                $"Program Copyright: {ParameterConstants.INDEX_START_STRING}{programInfo.Product}",
            });
        }
    }
}
