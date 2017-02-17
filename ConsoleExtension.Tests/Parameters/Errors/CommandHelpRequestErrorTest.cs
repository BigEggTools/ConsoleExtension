namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class CommandHelpRequestErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var commandAttribute = new CommandAttribute("Clone", "Clone a repository into a new directory");
            var propertyAttributes = new List<PropertyBaseAttribute>()
            {
                new StringPropertyAttribute("repository", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.")
            };

            var error = new CommandHelpRequestError(commandAttribute, propertyAttributes);
            Assert.AreEqual(ErrorType.CommandHelpRequest, error.ErrorType);
            Assert.AreEqual("Clone", error.CommandAttribute.Name);
            Assert.AreEqual(1, error.PropertyAttributes.Count());
            Assert.IsTrue(error.StopProcessing);
        }

        [TestMethod]
        public void ConstructorTest_PropertyAttributesEmpty()
        {
            var commandAttribute = new CommandAttribute("Clone", "Clone a repository into a new directory");
            var propertyAttributes = new List<PropertyBaseAttribute>();

            var error = new CommandHelpRequestError(commandAttribute, propertyAttributes);
            Assert.AreEqual(ErrorType.CommandHelpRequest, error.ErrorType);
            Assert.AreEqual("Clone", error.CommandAttribute.Name);
            Assert.AreEqual(0, error.PropertyAttributes.Count());
            Assert.IsTrue(error.StopProcessing);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_CommandAttributeNull()
        {
            var propertyAttributes = new List<PropertyBaseAttribute>();

            new CommandHelpRequestError(null, propertyAttributes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_PropertyAttributesNull()
        {
            var commandAttribute = new CommandAttribute("Clone", "Clone a repository into a new directory");

            new CommandHelpRequestError(commandAttribute, null);
        }
    }
}
