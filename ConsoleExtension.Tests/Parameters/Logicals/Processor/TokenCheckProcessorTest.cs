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
    public class TokenCheckProcessorTest : TestClassBase
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.TokenCheck);
            Assert.IsNotNull(processor);
        }

        [TestMethod]
        public void CanProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.TokenCheck);
            var context = new ProcessorContext(new List<string>() { "--help" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new HelpToken()
            };
            Assert.IsTrue(processor.CanProcess(context));
        }

        [TestMethod]
        public void CanProcessTest_TokensNull()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.TokenCheck);
            var context = new ProcessorContext(new List<string>() { "--help" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = null;
            Assert.IsFalse(processor.CanProcess(context));
        }

        [TestMethod]
        public void CanProcessTest_TokensEmpty()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.TokenCheck);
            var context = new ProcessorContext(new List<string>() { "--help" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>();
            Assert.IsFalse(processor.CanProcess(context));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ProcessTest_CannotProcess()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.TokenCheck);
            var context = new ProcessorContext(new List<string>() { "--help" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>();
            processor.Process(context);
        }

        [TestMethod]
        public void ProcessTest_DuplicateProperty()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.TokenCheck);
            var context = new ProcessorContext(new List<string>() { "--help", "--help" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new HelpToken(),
                new HelpToken()
            };

            processor.Process(context);

            Assert.IsTrue(context.Errors.Any(error => error.ErrorType == ErrorType.DuplicateProperty));
        }

        [TestMethod]
        public void ProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.TokenCheck);
            var context = new ProcessorContext(new List<string>() { "--help" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new HelpToken()
            };

            processor.Process(context);

            Assert.IsFalse(context.Errors.Any());
        }
    }
}
