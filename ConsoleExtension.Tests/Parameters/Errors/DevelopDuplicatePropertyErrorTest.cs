namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class DevelopDuplicatePropertyErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new DevelopDuplicatePropertyError("GitClone", "rep", "Repository", "Branch");
            Assert.AreEqual(ErrorType.Develop_DuplicateProperty, error.ErrorType);
            Assert.AreEqual("GitClone", error.TypeName);
            Assert.AreEqual("rep", error.AttributeName);
            Assert.AreEqual("Repository", error.PropertyName1);
            Assert.AreEqual("Branch", error.PropertyName2);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
