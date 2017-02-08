namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class UnknownCommandError : Error
    {
        public UnknownCommandError(string commandName)
            : base(ErrorType.UnKnownCommand, true)
        {
            CommandName = commandName;
        }

        public string CommandName { get; private set; }
    }
}
