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

        public string TypeName { get; set; }
        public string PropertyName { get; set; }
        public string AttributePropertyName { get; set; }
        public InvalidType InvalidType { get; set; }
        public string Regex { get; set; }
    }
}
