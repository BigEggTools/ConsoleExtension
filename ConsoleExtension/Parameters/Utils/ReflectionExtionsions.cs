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

        public static IDictionary<PropertyBaseAttribute, PropertyInfo> GetPropertyAttributes(this Type type)
        {
            return type.GetProperties()
                       .Select(property => new
                       {
                           Attribute = property.GetCustomAttributes(typeof(PropertyBaseAttribute), true)
                                               .FirstOrDefault() as PropertyBaseAttribute,
                           Property = property
                       })
                       .Where(pair => pair.Attribute != null)
                       .ToDictionary(pair => pair.Attribute, pair => pair.Property);
        }
    }
}
