namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class UnknownCommandError : Error
    {
        public UnknownCommandError(string commandName, IEnumerable<CommandAttribute> commandAttributes)
            : base(ErrorType.UnknownCommand, true)
        {
            if (commandAttributes == null || !commandAttributes.Any())
            {
                throw new ArgumentException("commandAttributes");
            }

            CommandName = commandName;
            CommandAttributes = commandAttributes;
        }

        public string CommandName { get; private set; }

        public IEnumerable<CommandAttribute> CommandAttributes { get; private set; }
    }
}
