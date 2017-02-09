namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class InvalidCommandErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new InvalidCommandError("GitPush");
            Assert.AreEqual(ErrorType.InvalidCommand, error.ErrorType);
            Assert.AreEqual("GitPush", error.TypeName);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
