namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class VersionRequestErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new VersionRequestError();
            Assert.AreEqual(ErrorType.VersionRequest, error.ErrorType);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
