namespace BigEgg.Tools.ConsoleExtension.Parameters.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class ReflectionExtionsion
    {
        public static CommandAttribute GetCommand(this Type type)
        {
            var attribute = type.GetTypeInfo()
                                .GetCustomAttributes(typeof(CommandAttribute), false)
                                .FirstOrDefault();
            return attribute == null
                ? null
                : (CommandAttribute)attribute;
        }

        public static IList<PropertyBaseAttribute> GetAttributes(this Type type)
        {
            return type.GetCustomAttributes(typeof(PropertyAttributes), true)
                       .Select(attribute => (PropertyBaseAttribute)attribute)
                       .ToList();
        }
    }
}
