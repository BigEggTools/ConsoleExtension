using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    using BigEgg.Tools.ConsoleExtension.Parameters.Output.Text;
    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [TestClass]
    public class TextBuilderTest : TestClassBase
    {
        private readonly CompositionContainer mockContainer;
        private Mock<IProgramInfo> mockProgramInfo = new Mock<IProgramInfo>();
        private Mock<IOutputFormat> mockOutputFormat = new Mock<IOutputFormat>();

        public TextBuilderTest()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new TypeCatalog(
                typeof(TextBuilder)
            ));
            mockContainer = new CompositionContainer(catalog);
            mockContainer.ComposeExportedValue(mockProgramInfo.Object);
            mockContainer.ComposeExportedValue(mockOutputFormat.Object);

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
            mockOutputFormat.Reset();
        }

        [TestMethod]
        public void ConstructorTest()
        {
            var textBuilder = Container.GetExportedValue<ITextBuilder>();
            Assert.IsNotNull(textBuilder);
        }

        [TestMethod]
        public void BuildTest_VersionRequest()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            mockProgramInfo.SetupGet(info => info.Copyright).Returns("Copyright");
            mockProgramInfo.SetupGet(info => info.Product).Returns("Product");
            var mockVersionInfo = MockVersionInfo();

            var parseResult = new ParseFailedResult(new List<Error>() { new VersionRequestError() });
            var output = textBuilder.Build(parseResult);
            Assert.AreEqual("TestData", output);

            mockVersionInfo.Verify(mock => mock.Format(
                It.Is<string>(p => p == "Title"),
                It.Is<string>(p => p == "Version"),
                It.Is<string>(p => p == "Copyright"),
                It.Is<string>(p => p == "Product"),
                It.Is<int>(p => p == 80)
            ), Times.Once);
        }

        [TestMethod]
        public void BuildTest_DuplicateProperty()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var mockApplicationHeader = MockApplicationHeader();
            var mockErrorHeader = MockErrorHeader();
            var mockDuplicateProperty = MockDuplicateProperty();

            var parseResult = new ParseFailedResult(new List<Error>() { new DuplicatePropertyError("help") });
            var output = textBuilder.Build(parseResult);
            Assert.AreEqual($"TestData{Environment.NewLine}TestData{Environment.NewLine}TestData{Environment.NewLine}", output);

            mockApplicationHeader.Verify(mock => mock.Format(
                It.Is<string>(p => p == "Title"),
                It.Is<string>(p => p == "Version"),
                It.Is<int>(p => p == 80)
            ), Times.Once);
            mockErrorHeader.Verify(mock => mock.Format(
                It.Is<int>(p => p == 80)
            ), Times.Once);
            mockDuplicateProperty.Verify(mock => mock.Format(
                It.Is<string>(p => p == "help"),
                It.Is<int>(p => p == 80)
            ), Times.Once);
        }


        private Mock<VersionInfo> MockVersionInfo()
        {
            var mockVersionInfo = new Mock<VersionInfo>();
            mockOutputFormat.Setup(outputFormat => outputFormat.VERSION_INFO).Returns(mockVersionInfo.Object);
            mockVersionInfo.Setup(versionInfo => versionInfo.Format(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                           .Returns("TestData");
            return mockVersionInfo;
        }

        private Mock<ApplicationHeader> MockApplicationHeader()
        {
            var mockApplicationHeader = new Mock<ApplicationHeader>();
            mockOutputFormat.Setup(outputFormat => outputFormat.APPLICATION_HEADER).Returns(mockApplicationHeader.Object);
            mockApplicationHeader.Setup(applicationHeader => applicationHeader.Format(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                                 .Returns("TestData");
            return mockApplicationHeader;
        }

        private Mock<ErrorHeader> MockErrorHeader()
        {
            var mockErrorHeader = new Mock<ErrorHeader>();
            mockOutputFormat.Setup(outputFormat => outputFormat.ERROR_HEADER).Returns(mockErrorHeader.Object);
            mockErrorHeader.Setup(errorHeader => errorHeader.Format(It.IsAny<int>()))
                                 .Returns("TestData");
            return mockErrorHeader;
        }

        private Mock<DuplicateProperty> MockDuplicateProperty()
        {
            var mockDuplicateProperty = new Mock<DuplicateProperty>();
            mockOutputFormat.Setup(outputFormat => outputFormat.DUPLICATE_PROPERTY).Returns(mockDuplicateProperty.Object);
            mockDuplicateProperty.Setup(duplicateProperty => duplicateProperty.Format(It.IsAny<string>(), It.IsAny<int>()))
                                 .Returns("TestData");
            return mockDuplicateProperty;
        }
    }
}
