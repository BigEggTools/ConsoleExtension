namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals.Processor
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [Export(typeof(IProcessor))]
    internal class ExtractCommandProcessor : IProcessor
    {
        public ProcessorType ProcessorType { get { return ProcessorType.ExtractCommand; } }

        public bool CanProcess(ProcessorContext context)
        {
            return true;
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
                var commandName = context.CaseSensitive ? commandToken.Value : commandToken.Value.ToUpper();
                var existCommands = context.Types.Select(type => new { type.GetCommandAttributes().Name, Type = type })
                                 .ToDictionary(c => context.CaseSensitive ? c.Name : c.Name.ToUpper(), c => c.Type);
                if (existCommands.ContainsKey(commandName))
                {
                    var commandType = existCommands[commandName];
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
