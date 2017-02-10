namespace BigEgg.Tools.ConsoleExtension.Parameters.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal static class StringExtensions
    {
        public static string FormatWithIndex(this string formatString, int maximumDisplayWidth)
        {
            return FormatWithIndex(new string[] { formatString }, maximumDisplayWidth);
        }

        public static string FormatWithIndex(this string[] formatStrings, int maximumDisplayWidth)
        {
            return formatStrings.Select(line =>
            {
                return ConcatWithWidth(line, maximumDisplayWidth);
            }).Join();
        }


        private static string ConcatWithWidth(string format, int maximumDisplayWidth)
        {
            var result = format;

            var index = result.IndexOf(ParameterConstants.INDEX_START_STRING);
            result = result.Replace(ParameterConstants.INDEX_START_STRING, "");

            var stringBuilder = new StringBuilder();
            if (index == -1)
            {
                for (int i = 0; i <= result.Length / maximumDisplayWidth; i++)
                {
                    stringBuilder.AppendLine(
                        result.Substring(
                            i * maximumDisplayWidth,
                            Math.Min(result.Length - i * maximumDisplayWidth, maximumDisplayWidth)));
                }
            }
            else
            {
                var valueLenght = (maximumDisplayWidth - index);
                if (valueLenght <= 0) { throw new FormatException(); }

                for (int i = 0; i <= (result.Length - index) / valueLenght; i++)
                {
                    if (i == 0)
                    {
                        stringBuilder.AppendLine(
                            result.Substring(0, Math.Min(result.Length, valueLenght + index)));
                    }
                    else
                    {
                        stringBuilder.Append(' ', index);
                        stringBuilder.AppendLine(
                             result.Substring(
                                index + i * valueLenght,
                                Math.Min(result.Length - index - i * valueLenght, valueLenght)));
                    }
                }
            }

            return stringBuilder.ToString();
        }

        private static string Join(this IEnumerable<string> messages)
        {
            return string.Join(
                string.Empty,
                messages.Select(message =>
                    message.EndsWith(Environment.NewLine)
                    ? message
                    : message + Environment.NewLine));
        }
    }
}
