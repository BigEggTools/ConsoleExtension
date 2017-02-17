namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    using System;
    using System.Collections.Generic;

    internal class CommandHelpRequestError : Error
    {
        public CommandHelpRequestError(CommandAttribute commandAttribute, IEnumerable<PropertyBaseAttribute> propertyAttributes)
            : base(ErrorType.CommandHelpRequest, true)
        {
            if (commandAttribute == null) { throw new ArgumentException("commandAttribute"); }
            if (propertyAttributes == null) { throw new ArgumentException("propertyAttributes"); }

            CommandAttribute = commandAttribute;
            PropertyAttributes = propertyAttributes;
        }


        public CommandAttribute CommandAttribute { get; private set; }

        public IEnumerable<PropertyBaseAttribute> PropertyAttributes { get; private set; }
    }
}
