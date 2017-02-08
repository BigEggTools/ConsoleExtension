namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    using System;

    internal class CommandHelpRequestError : Error
    {
        public CommandHelpRequestError(string commandName, Type commandType)
            : base(ErrorType.CommandHelpRequest, true)
        {
            CommandName = commandName;
            CommandType = commandType;
        }


        public string CommandName { get; private set; }

        public Type CommandType { get; private set; }
    }
}
