namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class DuplicatePropertyErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new DuplicatePropertyError("help");
            Assert.AreEqual(ErrorType.DuplicateProperty, error.ErrorType);
            Assert.AreEqual("help", error.PropertyName);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
