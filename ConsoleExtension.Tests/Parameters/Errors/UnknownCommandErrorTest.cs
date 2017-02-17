namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class UnknownCommandErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var commandAttributes = new List<CommandAttribute>()
            {
                new CommandAttribute("Clone", "Clone a repository into a new directory")
            };
            var error = new UnknownCommandError("push", commandAttributes);
            Assert.AreEqual(ErrorType.UnknownCommand, error.ErrorType);
            Assert.AreEqual("push", error.CommandName);
            Assert.IsTrue(error.StopProcessing);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_AttributeEmpty()
        {
            new HelpRequestError(new List<CommandAttribute>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_AttributeNull()
        {
            new HelpRequestError(null);
        }
    }
}
