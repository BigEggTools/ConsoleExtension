namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [Export(typeof(IProcessor))]
    internal class TokenCheckProcessor : IProcessor
    {
        public ProcessorType ProcessorType { get { return ProcessorType.TokenCheck; } }

        public bool CanProcess(ProcessorContext context)
        {
            return context.Tokens != null && context.Tokens.Any();
        }

        public void Process(ProcessorContext context)
        {
            if (!CanProcess(context)) { throw new InvalidOperationException(); }

            var names = new List<string>();
            foreach (var token in context.Tokens)
            {
                var tokenName = token.Name.ToUpper();
                if (names.Contains(tokenName))
                {
                    context.Errors.Add(new DuplicatePropertyError(tokenName));
                    return;
                }
                names.Add(tokenName);
            }
        }
    }
}
