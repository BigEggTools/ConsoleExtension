namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    using System.Linq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    internal class ArgumentProcessor : IProcessor
    {
        public ProcessorType ProcessorType { get { return ProcessorType.ArgumentCheck; } }

        public bool CanProcess(ProcessorContext context)
        {
            return true;
        }

        public void Process(ProcessorContext context)
        {
            if (!context.Arguments.Any())
            {
                context.Errors.Add(new EmptyInputError());
            }
        }
    }
}
