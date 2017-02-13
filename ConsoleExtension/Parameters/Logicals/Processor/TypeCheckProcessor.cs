namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Runtime.InteropServices;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

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
                var commandAttribute = type.GetCommandAttributes();
                var errors = commandAttribute.Validate(type.FullName);
                if (errors.Any())
                {
                    context.Errors.AddRange(errors);
                    continue;
                }

                var commandName = context.CaseSensitive ? commandAttribute.Name.ToUpper() : commandAttribute.Name;
                if (commandNames.ContainsKey(commandName))
                {
                    context.Errors.Add(new DevelopDuplicateCommandError(commandName, commandNames[commandName].FullName, type.FullName));
                }
                else
                {
                    commandNames.Add(commandName, type);
                }

                ValidatePropertyAttributes(type, context);
            }
        }

        private void ValidatePropertyAttributes(Type type, ProcessorContext context)
        {
            var names = new Dictionary<string, _PropertyInfo>();
            var pairs = type.GetPropertyAttributes();
            foreach (var pair in pairs)
            {
                var errors = pair.Key.Validate(type.FullName, pair.Value);
                if (errors.Any())
                {
                    context.Errors.AddRange(errors);
                    continue;
                }

                var shortName = context.CaseSensitive ? pair.Key.ShortName.ToUpper() : pair.Key.ShortName;
                var longName = context.CaseSensitive ? pair.Key.LongName.ToUpper() : pair.Key.LongName;
                if (names.ContainsKey(shortName))
                {
                    context.Errors.Add(new DevelopDuplicatePropertyError(type.FullName, shortName, names[shortName].Name, pair.Value.Name));
                }
                else if (names.ContainsKey(longName))
                {
                    context.Errors.Add(new DevelopDuplicatePropertyError(type.FullName, longName, names[longName].Name, pair.Value.Name));
                }
                else
                {
                    names.Add(shortName, pair.Value);
                    names.Add(longName, pair.Value);
                }
            }
        }
    }
}
