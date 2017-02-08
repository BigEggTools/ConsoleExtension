namespace BigEgg.Tools.ConsoleExtension.Parameters.Output.Text
{
    using System;
    using System.Collections.Generic;

    internal class EmptyInput : TextBase
    {
        private string[] formatStrings;

        public EmptyInput()
        {
            formatStrings = new string[]
            {
                $"No input, please use '--help' see the help document."
            };
        }

        public virtual string Format(int maximumDisplayWidth)
        {
            return Format(formatStrings, new List<Tuple<string, string>>(), maximumDisplayWidth);
        }
    }
}
