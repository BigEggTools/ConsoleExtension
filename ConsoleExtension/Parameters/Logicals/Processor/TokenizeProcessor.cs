namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System.ComponentModel.Composition;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens.Exceptions;

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
            return context.Arguments != null;
        }

        public void Process(ProcessorContext context)
        {
            try
            {
                context.Tokens = tokenizer.ToTokens(context.Arguments);
            }
            catch (DuplicatePropertyException ex)
            {
                context.Errors.Add(new DuplicatePropertyError(ex.PropertyName));
            }
        }
    }
}
