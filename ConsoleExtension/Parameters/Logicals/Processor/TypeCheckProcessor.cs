namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System.Linq;
    using System.ComponentModel.Composition;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;
    using System.Collections.Generic;
    using System;

    [Export(typeof(IProcessor))]
    internal class TypeCheckProcessor : IProcessor
    {
        public ProcessorType ProcessorType { get { return ProcessorType.TypeCheck; } }

        public bool CanProcess(ProcessorContext context)
        {
            return true;
        }

        public void Process(ProcessorContext context)
        {
            var commandNames = new Dictionary<string, Type>();
            foreach (var type in context.Types)
            {
                var commandAttribute = type.GetCommand();
                if (commandAttribute == null)
                {
                    context.Errors.Add(new InvalidCommandError(type.FullName));
                }
                else
                {
                    var commandName = commandAttribute.Name.ToUpper();
                    if (commandNames.ContainsKey(commandName))
                    {
                        context.Errors.Add(new DuplicateCommandError(commandName, commandNames[commandName].FullName, type.FullName));
                    }
                    else
                    {
                        commandNames.Add(commandName, type);
                    }
                }
            }
        }
    }
}
