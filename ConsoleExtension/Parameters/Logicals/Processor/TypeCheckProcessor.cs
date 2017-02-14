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
            var names = new Dictionary<string, string>();
            var pairs = type.GetPropertyAttributes();
            foreach (var pair in pairs)
            {
                var attribute = pair.Item1;
                var propertyInfo = pair.Item2;

                var errors = attribute.Validate(type.FullName, propertyInfo);
                if (errors.Any())
                {
                    context.Errors.AddRange(errors);
                    continue;
                }

                var shortName = context.CaseSensitive ? attribute.ShortName.ToUpper() : attribute.ShortName;
                var longName = context.CaseSensitive ? attribute.LongName.ToUpper() : attribute.LongName;
                if (names.ContainsKey(shortName))
                {
                    context.Errors.Add(new DevelopDuplicatePropertyError(type.FullName, shortName, names[shortName], propertyInfo.Name));
                }
                else if (names.ContainsKey(longName))
                {
                    context.Errors.Add(new DevelopDuplicatePropertyError(type.FullName, longName, names[longName], propertyInfo.Name));
                }
                else
                {
                    names.Add(shortName, propertyInfo.Name);
                    names.Add(longName, propertyInfo.Name);
                }
            }
        }
    }
}
