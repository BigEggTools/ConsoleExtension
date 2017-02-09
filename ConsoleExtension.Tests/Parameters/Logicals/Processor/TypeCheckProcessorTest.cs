namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Logicals.Processor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Logicals;
    using BigEgg.Tools.ConsoleExtension.Tests.Parameters.FakeParameters;

    [TestClass]
    public class TypeCheckProcessorTest : TestClassBase
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.TypeCheck);
            Assert.IsNotNull(processor);
        }

        [TestMethod]
        public void CanProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.TypeCheck);
            var context = new ProcessorContext(new List<string>(), new List<Type>() { typeof(GitClone) }, false);
            Assert.IsTrue(processor.CanProcess(context));
        }

        [TestMethod]
        public void ProcessTest_InvalidType()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.TypeCheck);
            var context = new ProcessorContext(new List<string>(), new List<Type>() { typeof(EmptyClass) }, false);
            processor.Process(context);

            Assert.IsTrue(context.Errors.Any(error => error.ErrorType == ErrorType.InvalidCommand));
        }

        [TestMethod]
        public void ProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.TypeCheck);
            var context = new ProcessorContext(new List<string>(), new List<Type>() { typeof(GitClone) }, false);
            processor.Process(context);

            Assert.IsFalse(context.Errors.Any());
        }
    }
}
