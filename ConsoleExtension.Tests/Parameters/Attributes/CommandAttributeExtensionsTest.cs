namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Attributes
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class CommandAttributeExtensionsTest
    {
        [TestMethod]
        public void ValidateTest()
        {
            var attribute = new CommandAttribute("Clone", "Clone a repository into a new directory");
            var errors = attribute.Validate("Git");
            Assert.IsFalse(errors.Any());
        }

        [TestMethod]
        public void ValidateTest_Null()
        {
            CommandAttribute attribute = null;
            var errors = attribute.Validate("Git");
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual(ErrorType.Develop_MissingCommand, errors.First().ErrorType);
        }

        [TestMethod]
        public void ValidateTest_EmptyName()
        {
            var errors = new CommandAttribute(null, "Clone a repository into a new directory").Validate("Git");
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidCommandError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Name", error.PropertyName);
            Assert.AreEqual("", error.Regex);

            errors = new CommandAttribute("", "Clone a repository into a new directory").Validate("Git");
            Assert.AreEqual(1, errors.Count);
            error = errors.First() as DevelopInvalidCommandError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Name", error.PropertyName);
            Assert.AreEqual("", error.Regex);

            errors = new CommandAttribute("   ", "Clone a repository into a new directory").Validate("Git");
            Assert.AreEqual(1, errors.Count);
            error = errors.First() as DevelopInvalidCommandError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Name", error.PropertyName);
            Assert.AreEqual("", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_NameToLong()
        {
            var errors = new CommandAttribute(new string('a', 17), "Clone a repository into a new directory").Validate("Git");
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidCommandError;
            Assert.AreEqual(InvalidType.TooLong, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Name", error.PropertyName);
            Assert.AreEqual("", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_NameNotMatchRegex()
        {
            var errors = new CommandAttribute("Clone~~", "Clone a repository into a new directory").Validate("Git");
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidCommandError;
            Assert.AreEqual(InvalidType.RegexInvalid, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Name", error.PropertyName);
            Assert.AreEqual("^[a-zA-Z0-9-]{1,16}$", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_EmptyHelpMessage()
        {
            var errors = new CommandAttribute("Clone", null).Validate("Git");
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidCommandError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("HelpMessage", error.PropertyName);
            Assert.AreEqual("", error.Regex);

            errors = new CommandAttribute("Clone", "").Validate("Git");
            Assert.AreEqual(1, errors.Count);
            error = errors.First() as DevelopInvalidCommandError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("HelpMessage", error.PropertyName);
            Assert.AreEqual("", error.Regex);

            errors = new CommandAttribute("Clone", "   ").Validate("Git");
            Assert.AreEqual(1, errors.Count);
            error = errors.First() as DevelopInvalidCommandError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("HelpMessage", error.PropertyName);
            Assert.AreEqual("", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_HelpMessageToLong()
        {
            var errors = new CommandAttribute("Clone", new string('a', 257)).Validate("Git");
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidCommandError;
            Assert.AreEqual(InvalidType.TooLong, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("HelpMessage", error.PropertyName);
            Assert.AreEqual("", error.Regex);
        }
    }
}
