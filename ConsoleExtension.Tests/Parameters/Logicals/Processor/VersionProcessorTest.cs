namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Logicals.Processor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Logicals;
    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;

    using BigEgg.Tools.ConsoleExtension.Tests.Parameters.FakeParameters;

    [TestClass]
    public class VersionProcessorTest : TestClassBase
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.Version);
            Assert.IsNotNull(processor);
        }

        [TestMethod]
        public void CanProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.Version);
            var context = new ProcessorContext(new List<string>() { "--version" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new VersionToken()
            };
            Assert.IsTrue(processor.CanProcess(context));
        }

        [TestMethod]
        public void CanProcessTest_NoVersionToken()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.Version);
            var context = new ProcessorContext(new List<string>() { "--help" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new HelpToken()
            };
            Assert.IsFalse(processor.CanProcess(context));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ProcessTest_CannotProcess()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.Version);
            var context = new ProcessorContext(new List<string>() { "--version" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new HelpToken()
            };
            processor.Process(context);
        }

        [TestMethod]
        public void ProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.Version);
            var context = new ProcessorContext(new List<string>() { "--version" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new VersionToken()
            };

            processor.Process(context);

            Assert.IsTrue(context.Errors.Any(error => error.ErrorType == ErrorType.VersionRequest));
        }
    }
}
