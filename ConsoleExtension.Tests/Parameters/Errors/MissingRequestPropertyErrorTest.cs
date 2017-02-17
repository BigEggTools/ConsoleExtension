namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class MissingRequestPropertyErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new MissingRequestPropertyError("Clone", new List<string> { "Repository" });
            Assert.AreEqual(ErrorType.MissingRequestProperty, error.ErrorType);
            Assert.IsTrue(error.StopProcessing);
            Assert.AreEqual("Clone", error.CommandName);
            Assert.AreEqual("Repository", error.PropertyNames.First());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_AttributeEmpty()
        {
            new MissingCommandError(new List<CommandAttribute>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_AttributeNull()
        {
            new MissingCommandError(null);
        }
    }
}
