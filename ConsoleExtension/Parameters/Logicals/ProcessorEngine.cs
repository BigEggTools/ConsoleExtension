namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;

    [Export]
    internal class ProcessorEngine : IProcessorEngine
    {
        private readonly IEnumerable<IProcessor> processors;
        private readonly ITokenizer tokenizer;

        private static readonly IDictionary<ProcessorType, int> priority;


        static ProcessorEngine()
        {
            priority = new Dictionary<ProcessorType, int>()
            {
                { ProcessorType.ArgumentCheck, 0 },
                { ProcessorType.Tokenize, 1 },
                { ProcessorType.Version, 2 },
                { ProcessorType.Help, 3 },
                { ProcessorType.ExtractCommand, 4 },
                { ProcessorType.CommandHelp, 5 },
                { ProcessorType.TypeParser, 6 },
            };
        }

        [ImportingConstructor]
        public ProcessorEngine([ImportMany] IEnumerable<IProcessor> processors, ITokenizer tokenizer)
        {
            this.processors = processors.OrderBy(processor => priority[processor.ProcessorType]);
            this.tokenizer = tokenizer;
        }


        public ParserResult Handle(IEnumerable<string> args, Type[] types, bool caseSensitive)
        {
            var context = new ProcessorContext(args, types, caseSensitive);
            foreach (var processor in processors)
            {
                if (processor.CanProcess(context))
                {
                    processor.Process(context);
                }

                if (context.Errors.Any(error => error.StopProcessing)) { break; }
            }
            if (context.Command != null)
            {
                return new ParseSuccessResult(context.Command, context.CommandType);
            }
            else
            {
                return new ParseFailedResult(context.Errors);
            }
        }
    }
}
