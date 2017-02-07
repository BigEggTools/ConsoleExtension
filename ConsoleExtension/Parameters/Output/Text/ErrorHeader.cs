namespace BigEgg.Tools.ConsoleExtension.Parameters.Output.Text
{
    using System;
    using System.Collections.Generic;

    internal class ErrorHeader : TextBase
    {
        private string[] formatStrings;

        public ErrorHeader()
        {
            formatStrings = new string[]
            {
                $"Some error(s) happened, please see the detail error messages:"
            };
        }

        public string Format(int maximumDisplayWidth)
        {
            return Format(formatStrings, new List<Tuple<string, string>>(), maximumDisplayWidth);
        }
    }
}
