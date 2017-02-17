namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class DevelopPropertyCannotWriteError : Error
    {
        public DevelopPropertyCannotWriteError(string typeName, string propertyName)
            : base(ErrorType.Develop_PropertyTypeCannotWrite, true)
        {
            TypeName = typeName;
            PropertyName = propertyName;
        }

        public string TypeName { get; private set; }
        public string PropertyName { get; private set; }
    }
}
