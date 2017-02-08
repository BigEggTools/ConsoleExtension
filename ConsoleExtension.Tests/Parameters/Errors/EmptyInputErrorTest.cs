namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class EmptyInputErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new EmptyInputError();
            Assert.AreEqual(ErrorType.EmptyInput, error.ErrorType);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
