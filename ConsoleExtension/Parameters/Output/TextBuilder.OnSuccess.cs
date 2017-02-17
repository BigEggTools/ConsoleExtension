namespace BigEgg.Tools.ConsoleExtension.Parameters.Output
{
    using System.Collections.Generic;

    using BigEgg.Tools.ConsoleExtension.Parameters.Results;

    internal partial class TextBuilder
    {
        private string OnSuccess(ParseSuccessResult result, int maximumDisplayWidth)
        {
            return BuildString(new List<string>()
            {
                ApplicationHeaderText
            }, maximumDisplayWidth);
        }
    }
}
