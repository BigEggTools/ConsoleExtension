namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Output.Text
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Output.Text;

    [TestClass]
    public class DuplicatePropertyTest
    {
        [TestMethod]
        public void FormatTest()
        {
            var header = new DuplicateProperty();
            var text = header.Format("help", 80);

            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.AreEqual(2, lines.Length);
            Assert.IsTrue(lines[0].Contains("'help'"));
        }
    }
}
