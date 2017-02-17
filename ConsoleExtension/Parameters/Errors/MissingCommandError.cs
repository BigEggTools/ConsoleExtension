namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class MissingCommandError : Error
    {
        public MissingCommandError(IEnumerable<CommandAttribute> commandAttributes)
            : base(ErrorType.MissingCommand, true)
        {
            if (commandAttributes == null || !commandAttributes.Any())
            {
                throw new ArgumentException("commandAttributes");
            }
            CommandAttributes = commandAttributes;
        }

        public IEnumerable<CommandAttribute> CommandAttributes { get; private set; }
    }
}
