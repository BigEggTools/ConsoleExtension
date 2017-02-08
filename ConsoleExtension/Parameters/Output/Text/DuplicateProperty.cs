namespace BigEgg.Tools.ConsoleExtension.Parameters.Output.Text
{
    using System;
    using System.Collections.Generic;

    internal class DuplicateProperty : TextBase
    {
        private const string NAME_REPLACER = "|property_name|";
        private string[] formatStrings;

        public DuplicateProperty()
        {
            formatStrings = new string[]
            {
                $"Property '{NAME_REPLACER}' had been set multiple times"
            };
        }

        public virtual string Format(string propertyName, int maximumDisplayWidth)
        {
            return Format(formatStrings, new List<Tuple<string, string>>()
            {
                new Tuple<string, string>(NAME_REPLACER, propertyName)
            }, maximumDisplayWidth);
        }
    }
}
