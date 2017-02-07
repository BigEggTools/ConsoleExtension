namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Output.Text
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Output.Text;

    [TestClass]
    public class HeaderTest
    {
        [TestMethod]
        public void FormatTest()
        {
            var header = new Header();
            var text = header.Format("ConsoleExtension", "1.0.0", 80);

            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.AreEqual(2, lines.Length);
            Assert.AreEqual("ConsoleExtension: v1.0.0", lines[0]);
        }
    }
}
