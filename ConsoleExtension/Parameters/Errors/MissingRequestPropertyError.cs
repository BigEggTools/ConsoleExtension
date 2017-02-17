using System.Collections.Generic;

namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class MissingRequestPropertyError : Error
    {
        public MissingRequestPropertyError(string commandName, IEnumerable<string> propertyNames)
            : base(ErrorType.MissingRequestProperty, true)
        {
            CommandName = commandName;
            PropertyNames = propertyNames;
        }

        public string CommandName { get; private set; }
        public IEnumerable<string> PropertyNames { get; private set; }
    }
}
