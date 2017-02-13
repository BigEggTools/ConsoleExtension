namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class DevelopDuplicatePropertyError : Error
    {
        public DevelopDuplicatePropertyError(string typeName, string attributeName, string propertyName1, string propertyName2)
            : base(ErrorType.Develop_DuplicateProperty, true)
        {
            TypeName = typeName;
            AttributeName = attributeName;
            PropertyName1 = propertyName1;
            PropertyName2 = propertyName2;
        }


        public string TypeName { get; private set; }
        public string AttributeName { get; private set; }
        public string PropertyName1 { get; private set; }
        public string PropertyName2 { get; private set; }
    }
}
