namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [Export(typeof(IProcessor))]
    internal class CommandHelpProcessor : IProcessor
    {
        public ProcessorType ProcessorType { get { return ProcessorType.CommandHelp; } }

        public bool CanProcess(ProcessorContext context)
        {
            return context.Tokens.Any(t => t.TokenType == TokenType.Help) &&
                context.CommandType != null;
        }

        public void Process(ProcessorContext context)
        {
            if (!CanProcess(context)) { throw new InvalidOperationException(); }
            var commandName = context.CommandType.GetCommandAttributes().Name;

            context.Errors.Add(new CommandHelpRequestError(
                commandName,
                context.CommandType
            ));
        }
    }
}
