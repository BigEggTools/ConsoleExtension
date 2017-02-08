namespace BigEgg.Tools.ConsoleExtension.Parameters.Errors
{
    internal class MissingCommandError : Error
    {
        public MissingCommandError()
            : base(ErrorType.MissingCommand, true)
        { }
    }
}
