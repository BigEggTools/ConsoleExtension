namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class InvalidCommandError : Error
    {
        public InvalidCommandError(string typeName)
            : base(ErrorType.InvalidCommand, true)
        {
            TypeName = typeName;
        }

        public string TypeName { get; set; }
    }
}
