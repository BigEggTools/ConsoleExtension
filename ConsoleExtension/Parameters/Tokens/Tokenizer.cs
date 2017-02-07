namespace BigEgg.Tools.ConsoleExtension.Parameters.Tokens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens.Exceptions;

    internal static class Tokenizer
    {
        public static IList<Token> ToTokens(this IList<string> args)
        {
            var result = new Dictionary<string, Token>();
            var index = 0;
            if (!WithPrefixDash(args[0]))
            {
                AddToken(result, new CommandToken(args.First()));
                index++;
            }

            var tokenName = string.Empty;
            for (; index < args.Count; index++)
            {
                if (WithPrefixDash(args[index]))
                {
                    if (!string.IsNullOrWhiteSpace(tokenName))
                    {
                        var flagToken = ParseFlagToken(tokenName);
                        AddToken(result, flagToken);
                        tokenName = string.Empty;
                    }
                    tokenName = GetTokenName(args[index]);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(tokenName))
                    {
                        AddToken(result, new UnknownToken(args[index]));
                    }
                    else
                    {
                        AddToken(result, new PropertyToken(tokenName, args[index]));
                        tokenName = string.Empty;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(tokenName))
            {
                var flagToken = ParseFlagToken(tokenName);
                AddToken(result, flagToken);
            }

            return result.Values.ToList();
        }

        private static bool WithPrefixDash(string arg)
        {
            return arg.StartsWith(ParameterConstants.PROPERTY_NAME_PREFIX_DASH);
        }

        private static string GetTokenName(string arg)
        {
            return arg.Substring(2);
        }

        private static Token ParseFlagToken(string tokenName)
        {
            if (tokenName.Equals(ParameterConstants.TOKEN_HELP_NAME, StringComparison.OrdinalIgnoreCase))
            {
                return new HelpToken();
            }

            if (tokenName.Equals(ParameterConstants.TOKEN_VERSION_NAME, StringComparison.OrdinalIgnoreCase))
            {
                return new VersionToken();
            }

            return new FlagToken(tokenName);
        }

        private static void AddToken(IDictionary<string, Token> tokens, Token token)
        {
            if (tokens.ContainsKey(token.Name)) { throw new DuplicatePropertyException(token.Name); }
            tokens.Add(token.Name, token);
        }
    }
}
