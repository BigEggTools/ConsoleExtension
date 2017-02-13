namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Output;
    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [TestClass]
    public class TextBuilderTest : TestClassBase
    {
        private readonly CompositionContainer mockContainer;
        private Mock<IProgramInfo> mockProgramInfo = new Mock<IProgramInfo>();

        public TextBuilderTest()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new TypeCatalog(
                typeof(TextBuilder)
            ));
            mockContainer = new CompositionContainer(catalog);
            mockContainer.ComposeExportedValue(mockProgramInfo.Object);

            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(mockContainer);
            mockContainer.Compose(batch);
        }

        protected override void OnTestInitialize()
        {
            mockProgramInfo.SetupGet(info => info.Title).Returns("Title");
            mockProgramInfo.SetupGet(info => info.Version).Returns("Version");
        }

        protected override void OnTestCleanup()
        {
            mockProgramInfo.Reset();
        }

        [TestMethod]
        public void ConstructorTest()
        {
            var textBuilder = Container.GetExportedValue<ITextBuilder>();
            Assert.IsNotNull(textBuilder);
        }

        [TestMethod]
        public void BuildTest_EmptyInput()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(new List<Error>() { new EmptyInputError() });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_DuplicateProperty()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(new List<Error>() { new DuplicateArgumentError("help") });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_VersionRequest()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            mockProgramInfo.SetupGet(info => info.Copyright).Returns("Copyright");
            mockProgramInfo.SetupGet(info => info.Product).Returns("Product");

            var parseResult = new ParseFailedResult(new List<Error>() { new VersionRequestError() });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }
    }
}
