namespace BigEgg.Tools.ConsoleExtension.Parameters.Tokens
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    [Export(typeof(ITokenizer))]
    internal class Tokenizer : ITokenizer
    {
        public IList<Token> ToTokens(IEnumerable<string> args)
        {
            var result = new List<Token>();
            if (!args.Any()) { return result; }

            var firstArgs = true;
            var tokenName = string.Empty;
            foreach (var argument in args)
            {
                if (firstArgs && !WithPrefixDash(argument))
                {
                    result.Add(new CommandToken(argument));
                    firstArgs = false;
                    continue;
                }
                if (firstArgs) { firstArgs = false; }

                if (WithPrefixDash(argument))
                {
                    if (!string.IsNullOrWhiteSpace(tokenName))
                    {
                        result.Add(ParseFlagToken(tokenName));
                        tokenName = string.Empty;
                    }
                    tokenName = GetTokenName(argument);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(tokenName)) { result.Add(new UnknownToken(argument)); }
                    else
                    {
                        result.Add(new PropertyToken(tokenName, argument));
                        tokenName = string.Empty;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(tokenName)) { result.Add(ParseFlagToken(tokenName)); }

            return result;
        }

        private bool WithPrefixDash(string arg)
        {
            return arg.StartsWith(ParameterConstants.PROPERTY_NAME_PREFIX_DASH);
        }

        private string GetTokenName(string arg)
        {
            return arg.Replace(ParameterConstants.PROPERTY_NAME_PREFIX_DASH, "");
        }

        private Token ParseFlagToken(string tokenName)
        {
            if (tokenName.Equals(
                ParameterConstants.TOKEN_HELP_NAME,
                StringComparison.OrdinalIgnoreCase)) { return new HelpToken(); }

            if (tokenName.Equals(
                ParameterConstants.TOKEN_VERSION_NAME,
                StringComparison.OrdinalIgnoreCase)) { return new VersionToken(); }

            return new FlagToken(tokenName);
        }
    }
}
