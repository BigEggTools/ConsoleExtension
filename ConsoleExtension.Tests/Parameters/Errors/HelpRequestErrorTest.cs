namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class HelpRequestErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new HelpRequestError();
            Assert.AreEqual(ErrorType.HelpRequest, error.ErrorType);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
