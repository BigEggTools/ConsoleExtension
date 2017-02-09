namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;

    [Export(typeof(IProcessor))]
    internal class TokenizeProcessor : IProcessor
    {
        private readonly ITokenizer tokenizer;

        [ImportingConstructor]
        public TokenizeProcessor(ITokenizer tokenizer)
        {
            this.tokenizer = tokenizer;
        }

        public ProcessorType ProcessorType { get { return ProcessorType.Tokenize; } }

        public bool CanProcess(ProcessorContext context)
        {
            return context.Arguments != null && context.Arguments.Any();
        }

        public void Process(ProcessorContext context)
        {
            if (!CanProcess(context)) { throw new InvalidOperationException(); }

            context.Tokens = tokenizer.ToTokens(context.Arguments);
        }
    }
}
