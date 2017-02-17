namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals.Processor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [Export(typeof(IProcessor))]
    internal class TypeParseProcessor : IProcessor
    {
        private readonly IDictionary<PropertyAttributeType, Func<object, PropertyBaseAttribute, PropertyInfo, bool, IList<Token>, bool>> propertyHandlers;

        public TypeParseProcessor()
        {
            propertyHandlers = new Dictionary<PropertyAttributeType, Func<object, PropertyBaseAttribute, PropertyInfo, bool, IList<Token>, bool>>();
            propertyHandlers.Add(PropertyAttributeType.String, HandleStringProperty);
        }


        public ProcessorType ProcessorType { get { return ProcessorType.TypeParse; } }

        public bool CanProcess(ProcessorContext context)
        {
            return true;
        }

        public void Process(ProcessorContext context)
        {
            var commandAttribute = context.CommandType.GetCommandAttributes();
            var propertyMetaList = context.CommandType.GetPropertyAttributes();

            object command = Activator.CreateInstance(context.CommandType);

            var errorPropertyName = new List<string>();
            foreach (var meta in propertyMetaList)
            {
                var success = propertyHandlers[meta.Item1.PropertyAttributeType](command, meta.Item1, meta.Item2, context.CaseSensitive, context.Tokens);
                if (!success)
                {
                    errorPropertyName.Add(meta.Item1.LongName);
                }
            }

            if (errorPropertyName.Any())
            {
                context.Errors.Add(new MissingRequestPropertyError(commandAttribute.Name, errorPropertyName));
            }
            else
            {
                context.Command = command;
            }
        }

        private bool HandleStringProperty(
            object command,
            PropertyBaseAttribute attributeBase,
            PropertyInfo propertyInfo,
            bool caseSensitive,
            IList<Token> tokens)
        {
            var attribute = attributeBase as StringPropertyAttribute;
            var token = tokens.FirstOrDefault(t => t.TokenType == TokenType.Property
                                                && (t.Name.Equals(attribute.LongName, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)
                                                 || t.Name.Equals(attribute.ShortName, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)));
            if (token != null)
            {
                propertyInfo.SetValue(command, token.Value);
                return true;
            }
            if (attribute.Required && string.IsNullOrWhiteSpace(attribute.DefaultValue)) { return false; }
            if (attribute.Required && !string.IsNullOrWhiteSpace(attribute.DefaultValue))
            {
                propertyInfo.SetValue(command, attribute.DefaultValue);
            }
            return true;
        }
    }
}
