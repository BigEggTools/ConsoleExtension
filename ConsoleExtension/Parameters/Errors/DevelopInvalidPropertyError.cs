namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class DevelopInvalidPropertyError : Error
    {
        public DevelopInvalidPropertyError(string typeName, string propertyName, string attributePropertyName, InvalidType invalidType, string regex = "")
            : base(ErrorType.Develop_InvalidProperty, true)
        {
            TypeName = typeName;
            PropertyName = propertyName;
            AttributePropertyName = attributePropertyName;
            InvalidType = invalidType;
            Regex = regex;
        }

        public string TypeName { get; private set; }
        public string PropertyName { get; private set; }
        public string AttributePropertyName { get; private set; }
        public InvalidType InvalidType { get; private set; }
        public string Regex { get; private set; }
    }
}
