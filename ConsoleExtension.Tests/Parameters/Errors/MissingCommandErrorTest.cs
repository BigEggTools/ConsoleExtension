namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class MissingCommandErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new MissingCommandError();
            Assert.AreEqual(ErrorType.MissingCommand, error.ErrorType);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
