namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Output.Text
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ConsoleExtension.Parameters.Output.Text;

    [TestClass]
    public class VersionInfoTest
    {
        [TestMethod]
        public void FormatTest()
        {
            var versionInfo = new VersionInfo();
            var text = versionInfo.Format("ConsoleExtension", "1.0.0", "BigEgg.Tools", "Copyright c BigEgg 2017", 80);

            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.AreEqual(6, lines.Length);
        }
    }
}
