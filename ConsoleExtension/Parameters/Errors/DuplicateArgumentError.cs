namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class DuplicateArgumentError : Error
    {
        public DuplicateArgumentError(string propertyName)
            : base(ErrorType.DuplicateProperty, true)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}
