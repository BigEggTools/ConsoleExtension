namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class DevelopDuplicateCommandError : Error
    {
        public DevelopDuplicateCommandError(string commandName, string typeName1, string typeName2)
            : base(ErrorType.Develop_DuplicateCommand, true)
        {
            CommandName = commandName;
            TypeName1 = typeName1;
            TypeName2 = typeName2;
        }


        public string CommandName { get; private set; }
        public string TypeName1 { get; private set; }
        public string TypeName2 { get; private set; }
    }
}
