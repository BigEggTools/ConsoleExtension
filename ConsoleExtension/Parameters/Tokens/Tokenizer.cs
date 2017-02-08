namespace BigEgg.Tools.ConsoleExtension.Parameters.Tokens
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens.Exceptions;

    [Export(typeof(ITokenizer))]
    internal class Tokenizer : ITokenizer
    {
        public IList<Token> ToTokens(IEnumerable<string> args)
        {
            var result = new Dictionary<string, Token>();
            if (!args.Any()) { return result.Values.ToList(); }

            var firstArgs = true;
            var tokenName = string.Empty;
            foreach (var argument in args)
            {
                if (firstArgs && !WithPrefixDash(argument))
                {
                    AddToken(result, new CommandToken(argument));
                    firstArgs = false;
                    continue;
                }
                if (firstArgs)
                {
                    firstArgs = false;
                }

                if (WithPrefixDash(argument))
                {
                    if (!string.IsNullOrWhiteSpace(tokenName))
                    {
                        var flagToken = ParseFlagToken(tokenName);
                        AddToken(result, flagToken);
                        tokenName = string.Empty;
                    }
                    tokenName = GetTokenName(argument);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(tokenName))
                    {
                        AddToken(result, new UnknownToken(argument));
                    }
                    else
                    {
                        AddToken(result, new PropertyToken(tokenName, argument));
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

        private bool WithPrefixDash(string arg)
        {
            return arg.StartsWith(ParameterConstants.PROPERTY_NAME_PREFIX_DASH);
        }

        private string GetTokenName(string arg)
        {
            return arg.Substring(2);
        }

        private Token ParseFlagToken(string tokenName)
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

        private void AddToken(IDictionary<string, Token> tokens, Token token)
        {
            if (tokens.ContainsKey(token.Name)) { throw new DuplicatePropertyException(token.Name); }
            tokens.Add(token.Name, token);
        }
    }
}
