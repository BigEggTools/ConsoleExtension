namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [Export(typeof(IProcessor))]
    internal class VersionProcessor : IProcessor
    {
        public ProcessorType ProcessorType { get { return ProcessorType.Version; } }

        public bool CanProcess(ProcessorContext context)
        {
            return context.Tokens.Any(t => t.TokenType == TokenType.Version);
        }

        public void Process(ProcessorContext context)
        {
            if (!CanProcess(context)) { throw new InvalidOperationException(); }

            context.Errors.Add(new VersionRequestError());
        }
    }
}
