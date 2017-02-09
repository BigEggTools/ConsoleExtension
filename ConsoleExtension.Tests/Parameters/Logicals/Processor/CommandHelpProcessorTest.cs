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
    public class CommandHelpProcessorTest : TestClassBase
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.CommandHelp);
            Assert.IsNotNull(processor);
        }

        [TestMethod]
        public void CanProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.CommandHelp);
            var context = new ProcessorContext(new List<string>() { "clone", "--help" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new CommandToken("clone"),
                new HelpToken()
            };
            context.CommandType = typeof(GitClone);
            Assert.IsTrue(processor.CanProcess(context));
        }

        [TestMethod]
        public void CanProcessTest_NoHelpToken()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.CommandHelp);
            var context = new ProcessorContext(new List<string>() { "--version" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new VersionToken()
            };
            context.CommandType = typeof(GitClone);
            Assert.IsFalse(processor.CanProcess(context));
        }

        [TestMethod]
        public void CanProcessTest_NoCommandToken()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.CommandHelp);
            var context = new ProcessorContext(new List<string>() { "--help" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new HelpToken()
            };
            context.CommandType = null;
            Assert.IsFalse(processor.CanProcess(context));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ProcessTest_CannotProcess()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.CommandHelp);
            var context = new ProcessorContext(new List<string>() { "--version" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new VersionToken()
            };
            processor.Process(context);
        }

        [TestMethod]
        public void ProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.CommandHelp);
            var context = new ProcessorContext(new List<string>() { "clone", "--help" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new CommandToken("clone"),
                new HelpToken()
            };
            context.CommandType = typeof(GitClone);

            processor.Process(context);

            Assert.IsTrue(context.Errors.Any(error => error.ErrorType == ErrorType.CommandHelpRequest));
        }
    }
}
