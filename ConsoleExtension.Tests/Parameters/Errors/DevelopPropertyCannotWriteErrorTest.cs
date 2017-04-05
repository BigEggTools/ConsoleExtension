namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class DevelopPropertyCannotWriteErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new DevelopPropertyCannotWriteError("GitClone", "Repository");
            Assert.AreEqual(ErrorType.Develop_PropertyTypeCannotWrite, error.ErrorType);
            Assert.AreEqual("GitClone", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
