namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    using BigEgg.Tools.ConsoleExtension.Tests.Parameters.FakeParameters;

    [TestClass]
    public class CommandHelpRequestErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new CommandHelpRequestError("clone", true, typeof(GitClone));
            Assert.AreEqual(ErrorType.CommandHelpRequest, error.ErrorType);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
