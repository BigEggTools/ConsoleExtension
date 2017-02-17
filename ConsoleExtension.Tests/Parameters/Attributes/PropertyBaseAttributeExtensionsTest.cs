namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Attributes
{
    using System.Linq;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class PropertyBaseAttributeExtensionsTest
    {
        private Mock<_PropertyInfo> mockPropertyInfo = new Mock<_PropertyInfo>();

        [TestCleanup]
        public void TestCleanup()
        {
            mockPropertyInfo.Reset();
        }

        [TestMethod]
        public void ValidateTest()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var attribute = new StringPropertyAttribute("repository", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.");
            var errors = attribute.Validate("Git", mockPropertyInfo.Object);
            Assert.IsFalse(errors.Any());
        }

        [TestMethod]
        public void ValidateTest_EmptyShortName()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var errors = new StringPropertyAttribute("repository", null, "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("ShortName", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);

            errors = new StringPropertyAttribute("repository", "", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("ShortName", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);

            errors = new StringPropertyAttribute("repository", "   ", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("ShortName", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_ShortNameToLong()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var errors = new StringPropertyAttribute("repository", "repo", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.TooLong, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("ShortName", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_ShortNameNotMatchRegex()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var errors = new StringPropertyAttribute("repository", "r-", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.RegexInvalid, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("ShortName", error.AttributePropertyName);
            Assert.AreEqual("^[a-zA-Z0-9_]{1,3}$", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_EmptyLongName()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var errors = new StringPropertyAttribute(null, "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("LongName", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);

            errors = new StringPropertyAttribute("", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("LongName", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);

            errors = new StringPropertyAttribute("   ", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("LongName", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_LongNameToLong()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var errors = new StringPropertyAttribute(new string('a', 17), "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.TooLong, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("LongName", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_LongNameNotMatchRegex()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var errors = new StringPropertyAttribute("repo-sitory", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.RegexInvalid, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("LongName", error.AttributePropertyName);
            Assert.AreEqual("^[a-zA-Z0-9_]{1,16}$", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_EmptyHelpMessage()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var errors = new StringPropertyAttribute("repository", "rep", null).Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("HelpMessage", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);

            errors = new StringPropertyAttribute("repository", "rep", "").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("HelpMessage", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);

            errors = new StringPropertyAttribute("repository", "rep", "   ").Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.Empty, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("HelpMessage", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_HelpMessageToLong()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var errors = new StringPropertyAttribute("repository", "rep", new string('a', 257)).Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopInvalidPropertyError;
            Assert.AreEqual(InvalidType.TooLong, error.InvalidType);
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("HelpMessage", error.AttributePropertyName);
            Assert.AreEqual("", error.Regex);
        }

        [TestMethod]
        public void ValidateTest_CannotWrite()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(false);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var attribute = new StringPropertyAttribute("repository", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.");
            var errors = attribute.Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopPropertyCannotWriteError;
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
        }
    }
}
