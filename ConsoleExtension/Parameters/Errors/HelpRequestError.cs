namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class HelpRequestError : Error
    {
        public HelpRequestError(IEnumerable<CommandAttribute> commandAttributes)
            : base(ErrorType.HelpRequest, true)
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
