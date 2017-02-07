namespace BigEgg.Tools.ConsoleExtension.Parameters.Tokens.Exceptions
{
    using System;

    internal class DuplicatePropertyException : Exception
    {
        public DuplicatePropertyException(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}
