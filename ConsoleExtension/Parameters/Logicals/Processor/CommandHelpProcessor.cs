namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    internal class CommandHelpProcessor : IProcessor
    {
        public ProcessorType ProcessorType { get { return ProcessorType.Help; } }

        public bool CanProcess(ProcessorContext context)
        {
            return context.Tokens.Any(t => t.TokenType == TokenType.Help) &&
                context.Tokens.Any(t => t.TokenType == TokenType.Command);
        }

        public void Process(ProcessorContext context)
        {
            if (!CanProcess(context)) { throw new InvalidOperationException(); }

            var commandToken = context.Tokens.First(t => t.TokenType == TokenType.Command);
            var existCommands = context.Types.Select(type => new { type.GetCommand().Name, Type = type })
                                             .ToDictionary(c => c.Name, c => c.Type);

            var commandType = existCommands[commandToken.Value];

            context.Errors.Add(new CommandHelpRequestError(
                commandToken.Name,
                commandType != null,
                commandType != null ? commandType : null
            ));
        }
    }
}
