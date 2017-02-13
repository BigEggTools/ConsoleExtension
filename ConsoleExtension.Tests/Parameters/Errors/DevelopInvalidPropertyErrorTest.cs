namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class DevelopInvalidPropertyErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new DevelopInvalidPropertyError("GitClone", "Repository", "Name", InvalidType.Empty);
            Assert.AreEqual(ErrorType.Develop_InvalidProperty, error.ErrorType);
            Assert.AreEqual("GitClone", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("Name", error.AttributePropertyName);
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("", error.Regex);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
