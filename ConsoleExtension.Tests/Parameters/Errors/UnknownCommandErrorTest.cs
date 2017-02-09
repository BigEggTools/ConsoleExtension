namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class UnknownCommandErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new UnknownCommandError("push");
            Assert.AreEqual(ErrorType.UnKnownCommand, error.ErrorType);
            Assert.AreEqual("push", error.CommandName);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
