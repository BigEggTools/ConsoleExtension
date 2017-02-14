namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class DuplicateArgumentErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new DuplicateArgumentError("help");
            Assert.AreEqual(ErrorType.DuplicateArgument, error.ErrorType);
            Assert.AreEqual("help", error.PropertyName);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
