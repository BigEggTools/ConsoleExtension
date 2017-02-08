namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals.Processor
{
    using System;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    internal class ExtractCommandProcessor : IProcessor
    {
        public ProcessorType ProcessorType { get { return ProcessorType.ExtractCommand; } }

        public bool CanProcess(ProcessorContext context)
        {
            return !context.Tokens.Any(t => t.TokenType == TokenType.Help);
        }

        public void Process(ProcessorContext context)
        {
            if (!CanProcess(context)) { throw new InvalidOperationException(); }

            var commandToken = context.Tokens.FirstOrDefault(t => t.TokenType == TokenType.Command);
            if (commandToken == null)
            {
                if (context.Types.Count() == 1)
                {
                    context.CommandType = context.Types.First();
                }
                else
                {
                    context.Errors.Add(new MissingCommandError());
                }
            }
            else
            {
                var existCommands = context.Types.Select(type => new { type.GetCommand().Name, Type = type })
                                 .ToDictionary(c => c.Name, c => c.Type);
                var commandType = existCommands[commandToken.Value];
                if (commandType != null)
                {
                    context.CommandType = commandType;
                }
                else
                {
                    context.Errors.Add(new UnknownCommandError(commandToken.Value));
                }
            }
        }
    }
}
