namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Output;
    using BigEgg.Tools.ConsoleExtension.Parameters.Results;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class TextBuilderTest : TestClassBase
    {
        [TestMethod]
        public void BuildTest_VersionRequest()
        {
            var textBuilder = Container.GetExportedValue<ITextBuilder>();

            var parseResult = new ParseFailedResult(new List<Error>() { new VersionRequestError() });
            var output = textBuilder.Build(parseResult);
            var lines = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            Assert.AreEqual(6, lines.Length);
        }
    }
}
