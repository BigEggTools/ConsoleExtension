namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;

    using BigEgg.Tools.ConsoleExtension.Parameters.Results;

    internal interface IProcessorEngine
    {
        ParserResult Handle(IEnumerable<string> args, Type[] types, bool caseSensitive);
    }
}