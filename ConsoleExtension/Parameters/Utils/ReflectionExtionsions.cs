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

        public static IList<PropertyBaseAttribute> GetPropertyAttributes(this Type type)
        {
            return type.GetProperties()
                       .Select(property => property.GetCustomAttributes(typeof(PropertyBaseAttribute), true)
                                                   .FirstOrDefault())
                       .Where(attribute => attribute != null)
                       .Select(attribute => attribute as PropertyBaseAttribute)
                       .ToList();
        }
    }
}
