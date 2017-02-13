namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class DevelopMissingCommandError : Error
    {
        public DevelopMissingCommandError(string typeName)
            : base(ErrorType.Develop_MissingCommand, true)
        {
            TypeName = typeName;
        }

        public string TypeName { get; set; }
    }
}
