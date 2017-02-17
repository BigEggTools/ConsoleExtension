namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    using System.Collections.Generic;

    internal class DevelopPropertyTypeMismatchError : Error
    {
        public DevelopPropertyTypeMismatchError(string typeName, string propertyName, string currentType, IList<string> supportedTypes)
            : base(ErrorType.Develop_PropertyTypeMismatch, true)
        {
            TypeName = typeName;
            PropertyName = propertyName;
            CurrentType = currentType;
            SupportedTypes = supportedTypes;
        }

        public string TypeName { get; private set; }
        public string PropertyName { get; private set; }
        public string CurrentType { get; private set; }
        public IList<string> SupportedTypes { get; private set; }
    }
}
