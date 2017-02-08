namespace BigEgg.Tools.ConsoleExtension.Parameters.Logicals
{
    internal interface IProcessor
    {
        ProcessorType ProcessorType { get; }

        void Process(ProcessorContext context);

        bool CanProcess(ProcessorContext context);
    }
}
