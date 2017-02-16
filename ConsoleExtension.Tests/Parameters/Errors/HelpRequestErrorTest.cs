namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class HelpRequestErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var commandAttributes = new List<CommandAttribute>()
            {
                new CommandAttribute("Clone", "Clone a repository into a new directory")
            };
            var error = new HelpRequestError(commandAttributes);
            Assert.AreEqual(ErrorType.HelpRequest, error.ErrorType);
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
