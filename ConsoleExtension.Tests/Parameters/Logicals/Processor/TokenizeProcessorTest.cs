namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Logicals.Processor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Logicals;
    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;
    using BigEgg.Tools.ConsoleExtension.Tests.Parameters.FakeParameters;

    [TestClass]
    public class TokenizeProcessorTest : TestClassBase
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.Tokenize);
            Assert.IsNotNull(processor);
        }

        [TestMethod]
        public void CanProcessTest()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.Tokenize);
            var context = new ProcessorContext(new List<string>() { "--help" }, new List<Type>() { typeof(GitClone) }, false);
            Assert.IsTrue(processor.CanProcess(context));
        }

        [TestMethod]
        public void CanProcessTest_ArgumentNull()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.Tokenize);
            var context = new ProcessorContext(null, new List<Type>() { typeof(GitClone) }, false);
            Assert.IsFalse(processor.CanProcess(context));
        }

        [TestMethod]
        public void CanProcessTest_ArgumentEmpty()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                     .FirstOrDefault(p => p.ProcessorType == ProcessorType.Tokenize);
            var context = new ProcessorContext(new List<string>(), new List<Type>() { typeof(GitClone) }, false);
            Assert.IsFalse(processor.CanProcess(context));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ProcessTest_CannotProcess()
        {
            var processor = Container.GetExportedValues<IProcessor>()
                                    .FirstOrDefault(p => p.ProcessorType == ProcessorType.Tokenize);
            var context = new ProcessorContext(new List<string>(), new List<Type>() { typeof(EmptyClass) }, false);
            processor.Process(context);
        }

        [TestClass]
        public class TokenizeTest : TestClassBase
        {
            private readonly CompositionContainer mockContainer;
            private Mock<ITokenizer> mockTokenizer = new Mock<ITokenizer>();

            public TokenizeTest()
            {
                AggregateCatalog catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new TypeCatalog(
                    typeof(TokenizeProcessor)
                ));
                mockContainer = new CompositionContainer(catalog);
                mockContainer.ComposeExportedValue(mockTokenizer.Object);

                CompositionBatch batch = new CompositionBatch();
                batch.AddExportedValue(mockContainer);
                mockContainer.Compose(batch);
            }

            protected override void OnTestCleanup()
            {
                mockTokenizer.Reset();
            }

            [TestMethod]
            public void ProcessTest_DuplicateProperty()
            {
                var processor = mockContainer.GetExportedValue<IProcessor>();
                var context = new ProcessorContext(new List<string>() { "--help", "--help" }, new List<Type>() { typeof(EmptyClass) }, false);
                mockTokenizer.Setup(tokenizer => tokenizer.ToTokens(It.IsAny<IList<string>>())).Returns(new List<Token>()
                {
                    new HelpToken(),
                    new HelpToken()
                });

                processor.Process(context);

                Assert.AreEqual(2, context.Tokens.Count);
                Assert.IsFalse(context.Errors.Any());
            }

            [TestMethod]
            public void ProcessTest()
            {
                var processor = mockContainer.GetExportedValue<IProcessor>();
                var context = new ProcessorContext(new List<string>() { "--help" }, new List<Type>() { typeof(GitClone) }, false);
                mockTokenizer.Setup(tokenizer => tokenizer.ToTokens(It.IsAny<IList<string>>())).Returns(new List<Token>()
                {
                    new HelpToken()
                });

                processor.Process(context);

                Assert.AreEqual(1, context.Tokens.Count);
                Assert.IsFalse(context.Errors.Any());
            }
        }
    }
}
