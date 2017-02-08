namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [Export(typeof(IProcessor))]
    internal class DefaultHelpProcessor : IProcessor
    {
        public ProcessorType ProcessorType { get { return ProcessorType.Help; } }


        public bool CanProcess(ProcessorContext context)
        {
            return context.Tokens.Any(t => t.TokenType == TokenType.Help) &&
                !context.Tokens.Any(t => t.TokenType == TokenType.Command);
        }

        public void Process(ProcessorContext context)
        {
            if (!CanProcess(context)) { throw new InvalidOperationException(); }

            context.Errors.Add(new HelpRequestError());
        }
    }
}
