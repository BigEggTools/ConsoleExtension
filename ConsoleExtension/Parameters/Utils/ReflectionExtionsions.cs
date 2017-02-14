namespace BigEgg.Tools.ConsoleExtension.Parameters.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class ReflectionExtionsions
    {
        public static CommandAttribute GetCommandAttributes(this Type type)
        {
            var attribute = type.GetTypeInfo()
                                .GetCustomAttributes(typeof(CommandAttribute), false)
                                .FirstOrDefault();
            return attribute == null
                ? null
                : (CommandAttribute)attribute;
        }

        public static IList<Tuple<PropertyBaseAttribute, PropertyInfo>> GetPropertyAttributes(this Type type)
        {
            return type.GetProperties()
                       .Select(property => new Tuple<PropertyBaseAttribute, PropertyInfo>(
                           property.GetCustomAttributes(typeof(PropertyBaseAttribute), true)
                                   .FirstOrDefault() as PropertyBaseAttribute,
                           property
                       ))
                       .Where(pair => pair.Item1 != null)
                       .ToList();
        }
    }
}
