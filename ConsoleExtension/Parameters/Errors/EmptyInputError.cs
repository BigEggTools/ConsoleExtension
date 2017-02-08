namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class EmptyInputError : Error
    {
        public EmptyInputError()
            : base(ErrorType.EmptyInput, true)
        { }
    }
}
