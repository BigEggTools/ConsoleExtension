namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Utils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [TestClass]
    public class ProgramInfoTest
    {
        [TestMethod]
        public void DataTest()
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(ProgramInfo.Default.Title));
            Assert.IsFalse(string.IsNullOrWhiteSpace(ProgramInfo.Default.Version));
            Assert.IsFalse(string.IsNullOrWhiteSpace(ProgramInfo.Default.Product));
            Assert.IsFalse(string.IsNullOrWhiteSpace(ProgramInfo.Default.Copyright));
        }
    }
}
