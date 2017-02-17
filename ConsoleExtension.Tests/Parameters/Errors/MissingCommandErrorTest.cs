namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class MissingCommandErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var commandAttributes = new List<CommandAttribute>()
            {
                new CommandAttribute("Clone", "Clone a repository into a new directory")
            };
            var error = new MissingCommandError(commandAttributes);
            Assert.AreEqual(ErrorType.MissingCommand, error.ErrorType);
            Assert.IsTrue(error.StopProcessing);
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
