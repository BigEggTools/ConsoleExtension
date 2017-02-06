namespace BigEgg.Tools.ConsoleExtension.Parameters.Tokens
{
    internal class UnknownToken : Token
    {
        public UnknownToken(string name)
            : base(name, TokenType.Unknown)
        { }
    }
}
