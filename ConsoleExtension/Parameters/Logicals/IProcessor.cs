namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;

    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;

    internal interface IProcessor
    {
        bool NeedType { get; }

        ParserResult Process(IList<Token> tokens, Type type, bool caseSensitive);
    }
}
