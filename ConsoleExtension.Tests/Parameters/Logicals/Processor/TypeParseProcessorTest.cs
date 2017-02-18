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
    public class TypeParseProcessorTest : TestClassBase
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.TypeParse);
            Assert.IsNotNull(processor);
        }

        [TestMethod]
        public void CanProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.TypeParse);
            var context = new ProcessorContext(new List<string>(), new List<Type>() { typeof(GitClone) }, false);
            Assert.IsTrue(processor.CanProcess(context));
        }

        [TestMethod]
        public void ProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.TypeParse);
            var context = new ProcessorContext(new List<string>() { "clone", "--repository", "https://abc.com" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new CommandToken("clone"),
                new PropertyToken("repository", "https://abc.com")
            };
            context.CommandType = typeof(GitClone);
            processor.Process(context);

            Assert.IsFalse(context.Errors.Any());
            Assert.IsNotNull(context.Command);

            var gitClone = context.Command as GitClone;
            Assert.AreEqual("https://abc.com", gitClone.Repository);
            Assert.IsFalse(gitClone.Recurse);
        }

        [TestMethod]
        public void ProcessTest_WithFlag()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.TypeParse);
            var context = new ProcessorContext(new List<string>() { "clone", "--repository", "https://abc.com", "--Recurse" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new CommandToken("clone"),
                new PropertyToken("repository", "https://abc.com"),
                new FlagToken("Recurse")
            };
            context.CommandType = typeof(GitClone);
            processor.Process(context);

            Assert.IsFalse(context.Errors.Any());
            Assert.IsNotNull(context.Command);

            var gitClone = context.Command as GitClone;
            Assert.AreEqual("https://abc.com", gitClone.Repository);
            Assert.IsTrue(gitClone.Recurse);
        }

        [TestMethod]
        public void ProcessTest_ShortName()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.TypeParse);
            var context = new ProcessorContext(new List<string>() { "clone", "--repository", "https://abc.com", "--Recurse" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new CommandToken("clone"),
                new PropertyToken("rep", "https://abc.com"),
                new FlagToken("r")
            };
            context.CommandType = typeof(GitClone);
            processor.Process(context);

            Assert.IsFalse(context.Errors.Any());
            Assert.IsNotNull(context.Command);

            var gitClone = context.Command as GitClone;
            Assert.AreEqual("https://abc.com", gitClone.Repository);
            Assert.IsTrue(gitClone.Recurse);
        }

        [TestMethod]
        public void ProcessTest_MissingRequestProperty()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.TypeParse);
            var context = new ProcessorContext(new List<string>() { "clone", "--error", "https://abc.com" }, new List<Type>() { typeof(GitClone) }, false);
            context.Tokens = new List<Token>()
            {
                new CommandToken("clone"),
                new PropertyToken("error", "https://abc.com")
            };
            context.CommandType = typeof(GitClone);
            processor.Process(context);

            Assert.IsTrue(context.Errors.Any(error => error.ErrorType == ErrorType.MissingRequestProperty));
            Assert.IsNull(context.Command);
        }
    }
}