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
            return FormatWithIndex(formatString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None), maximumDisplayWidth);
        }

        public static string FormatWithIndex(this string[] formatStrings, int maximumDisplayWidth)
        {
            return formatStrings.Select(line =>
            {
                return ConcatWithWidth(line, maximumDisplayWidth);
            }).Join();
        }

        public static string FillWithCharacter(this string message, int totalLength, char character)
        {
            if (message.Length >= totalLength) { return message; }

            return message + new string(character, totalLength - message.Length);
        }


        private static string ConcatWithWidth(string format, int maximumDisplayWidth)
        {
            var result = format;
            maximumDisplayWidth = maximumDisplayWidth - 1;

            var index = result.IndexOf(ParameterConstants.INDEX_START_STRING);
            if (index == -1) { return result; }

            var valueLenght = (maximumDisplayWidth - index);
            if (valueLenght <= 0) { throw new FormatException(); }

            result = result.Replace(ParameterConstants.INDEX_START_STRING, "");
            var stringBuilder = new StringBuilder();

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
