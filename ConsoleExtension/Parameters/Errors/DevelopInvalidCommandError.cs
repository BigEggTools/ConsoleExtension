namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class DevelopInvalidCommandError : Error
    {
        public DevelopInvalidCommandError(string typeName, string propertyName, InvalidType invalidType, string regex = "")
            : base(ErrorType.Develop_InvalidCommand, true)
        {
            TypeName = typeName;
            PropertyName = propertyName;
            InvalidType = invalidType;
            Regex = regex;
        }

        public string TypeName { get; private set; }
        public string PropertyName { get; private set; }
        public InvalidType InvalidType { get; private set; }
        public string Regex { get; private set; }
    }
}
