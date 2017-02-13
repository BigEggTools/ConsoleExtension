namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal class ProcessorContext
    {
        private readonly List<Error> errors;

        public ProcessorContext(IEnumerable<string> arguments, IEnumerable<Type> types, bool caseSensitive)
        {
            Arguments = arguments;
            Types = types;
            CaseSensitive = caseSensitive;

            errors = new List<Error>();
        }


        public IEnumerable<string> Arguments { get; private set; }

        public IEnumerable<Type> Types { get; private set; }

        public bool CaseSensitive { get; private set; }


        public List<Error> Errors { get { return errors; } }

        public IList<Token> Tokens { get; set; }

        public Type CommandType { get; set; }

        public object Command { get; set; }
    }
}
