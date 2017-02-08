namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System.Linq;
    using System.ComponentModel.Composition;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [Export(typeof(IProcessor))]
    internal class TypeCheckProcessor : IProcessor
    {
        public ProcessorType ProcessorType { get { return ProcessorType.TypeCheck; } }

        public bool CanProcess(ProcessorContext context)
        {
            return true;
        }

        public void Process(ProcessorContext context)
        {
            var invalidCommandType = context.Types.First(type => type.GetCommand() == null);
            if (invalidCommandType != null)
            {
                context.Errors.Add(new InvalidCommandError(invalidCommandType.FullName));
            }
        }
    }
}
