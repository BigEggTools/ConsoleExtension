namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Output
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;
    using BigEgg.Tools.ConsoleExtension.Parameters.Output;
    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;
    using BigEgg.Tools.ConsoleExtension.Parameters;

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

        [TestMethod]
        public void BuildTest_DevelopDuplicateCommand()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(new List<Error>() { new DevelopDuplicateCommandError("Clone", "GitClone", "GitPull") });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_DevelopDuplicateProperty()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(new List<Error>() { new DevelopDuplicatePropertyError("Clone", "rep", "Repository", "Master") });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_DevelopInvalidCommand()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(new List<Error>() { new DevelopInvalidCommandError("GitClone", "Name", InvalidType.Empty) });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));

            parseResult = new ParseFailedResult(new List<Error>() { new DevelopInvalidCommandError("GitClone", "Name", InvalidType.TooLong) });
            output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));

            parseResult = new ParseFailedResult(new List<Error>() { new DevelopInvalidCommandError("GitClone", "Name", InvalidType.RegexInvalid, "^[a-zA-Z0-9-]{1,16}$") });
            output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_DevelopInvalidProperty()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(new List<Error>() { new DevelopInvalidPropertyError("GitClone", "Repository", "LongName", InvalidType.Empty) });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));

            parseResult = new ParseFailedResult(new List<Error>() { new DevelopInvalidPropertyError("GitClone", "Repository", "LongName", InvalidType.TooLong) });
            output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));

            parseResult = new ParseFailedResult(new List<Error>() { new DevelopInvalidPropertyError("GitClone", "Repository", "LongName", InvalidType.RegexInvalid, "^[a-zA-Z0-9-]{1,16}$") });
            output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_DevelopMissingCommand()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(new List<Error>() { new DevelopMissingCommandError("GitClone") });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_DevelopPropertyCannotWrite()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(new List<Error>() { new DevelopPropertyCannotWriteError("GitClone", "Repository") });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_DevelopPropertyTypeMismatch()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(new List<Error>() { new DevelopPropertyTypeMismatchError("GitClone", "Repository", "Boolean", new List<string>() { "String" }) });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_HelpRequest()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(
                new List<Error>()
                {
                    new HelpRequestError(new List<CommandAttribute>()
                    {
                        new CommandAttribute("Clone", "Clone a repository into a new directory")
                    })
                });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_MissingCommand()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(
                new List<Error>()
                {
                    new MissingCommandError(new List<CommandAttribute>()
                    {
                        new CommandAttribute("Clone", "Clone a repository into a new directory")
                    })
                });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_UnknownCommand()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(
                new List<Error>()
                {
                    new UnknownCommandError("Error", new List<CommandAttribute>()
                    {
                        new CommandAttribute("Clone", "Clone a repository into a new directory")
                    })
                });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }

        [TestMethod]
        public void BuildTest_CommandHelpRequest()
        {
            var textBuilder = mockContainer.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(
                new List<Error>()
                {
                    new CommandHelpRequestError(
                        new CommandAttribute("Clone", "Clone a repository into a new directory"),
                        new List<PropertyBaseAttribute>()
                        {
                            new StringPropertyAttribute("repository", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.")
                        })
                });
            var output = textBuilder.Build(parseResult);
            Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        }
    }
}
