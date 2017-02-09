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
    public class ExtractCommandProcessorTest : TestClassBase
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.ExtractCommand);
            Assert.IsNotNull(processor);
        }

        [TestMethod]
        public void CanProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.ExtractCommand);
            var context = new ProcessorContext(new List<string>() { "clone", "--repository", "url" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new CommandToken("clone"),
                new PropertyToken("repository", "url")
            };
            Assert.IsTrue(processor.CanProcess(context));
        }

        [TestMethod]
        public void ProcessTest_WithoutCommand()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.ExtractCommand);
            var context = new ProcessorContext(new List<string>() { "--repository", "url" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new PropertyToken("repository", "url")
            };
            processor.Process(context);

            Assert.AreEqual(typeof(GitClone), context.CommandType);
            Assert.IsFalse(context.Errors.Any());
        }

        [TestMethod]
        public void ProcessTest_WithoutCommand_MultiType()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.ExtractCommand);
            var context = new ProcessorContext(new List<string>() { "--repository", "url" }, new List<Type>() { typeof(GitClone), typeof(GitPull) }, false);
            context.Tokens = new List<Token>()
            {
                new PropertyToken("repository", "url")
            };
            processor.Process(context);

            Assert.IsTrue(context.Errors.Any(error => error.ErrorType == ErrorType.MissingCommand));
        }

        [TestMethod]
        public void ProcessTest_WithCommand()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.ExtractCommand);
            var context = new ProcessorContext(new List<string>() { "clone", "--repository", "url" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new CommandToken("clone"),
                new PropertyToken("repository", "url")
            };
            processor.Process(context);

            Assert.AreEqual(typeof(GitClone), context.CommandType);
            Assert.IsFalse(context.Errors.Any());
        }

        [TestMethod]
        public void ProcessTest_WithCommand_NotExist()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.ExtractCommand);
            var context = new ProcessorContext(new List<string>() { "pull", "--repository", "url" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new CommandToken("pull"),
                new PropertyToken("repository", "url")
            };
            processor.Process(context);

            Assert.IsTrue(context.Errors.Any(error => error.ErrorType == ErrorType.UnKnownCommand));
        }
    }
}
