namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class DevelopInvalidCommandErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new DevelopInvalidCommandError("GitClone", "Name", InvalidType.Empty);
            Assert.AreEqual(ErrorType.Develop_InvalidCommand, error.ErrorType);
            Assert.AreEqual("GitClone", error.TypeName);
            Assert.AreEqual("Name", error.PropertyName);
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("", error.Regex);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
