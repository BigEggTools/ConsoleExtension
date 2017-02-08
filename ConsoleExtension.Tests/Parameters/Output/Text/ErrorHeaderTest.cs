namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Output.Text
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Output.Text;

    [TestClass]
    public class ErrorHeaderTest
    {
        [TestMethod]
        public void FormatTest()
        {
            var header = new ErrorHeader();
            var text = header.Format(80);

            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.AreEqual(2, lines.Length);
        }
    }
}
