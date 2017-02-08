namespace BigEgg.Tools.ConsoleExtension.Parameters.Tokens
{
    using System.Collections.Generic;

    internal interface ITokenizer
    {
        IList<Token> ToTokens(IEnumerable<string> args);
    }
}