namespace BigEgg.Tools.ConsoleExtension.Parameters.Utils
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class ReflectionExtionsion
    {
        public static CommandAttribute GetCommand(this Type type)
        {
            var attribute = type.GetTypeInfo()
                                .GetCustomAttributes(typeof(CommandAttribute), true)
                                .FirstOrDefault();
            return attribute == null
                ? null
                : (CommandAttribute)attribute;
        }
    }
}
