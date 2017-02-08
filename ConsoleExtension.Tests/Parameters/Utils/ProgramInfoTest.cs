namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Utils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [TestClass]
    public class ProgramInfoTest : TestClassBase
    {
        [TestMethod]
        public void DataTest()
        {
            var programInfo = Container.GetExportedValue<IProgramInfo>();
            Assert.IsFalse(string.IsNullOrWhiteSpace(programInfo.Title));
            Assert.IsFalse(string.IsNullOrWhiteSpace(programInfo.Version));
            Assert.IsFalse(string.IsNullOrWhiteSpace(programInfo.Product));
            Assert.IsFalse(string.IsNullOrWhiteSpace(programInfo.Copyright));
        }
    }
}
