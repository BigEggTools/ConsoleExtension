namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class DevelopMissingCommandErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new DevelopMissingCommandError("GitClone");
            Assert.AreEqual(ErrorType.Develop_MissingCommand, error.ErrorType);
            Assert.AreEqual("GitClone", error.TypeName);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
