namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class DevelopPropertyTypeMismatchErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new DevelopPropertyTypeMismatchError("GitClone", "Repository", "Boolean", new List<string>() { "string" });
            Assert.AreEqual(ErrorType.Develop_PropertyTypeMismatch, error.ErrorType);
            Assert.AreEqual("GitClone", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("Boolean", error.CurrentType);
            Assert.AreEqual("string", error.SupportedTypes.Single());
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
