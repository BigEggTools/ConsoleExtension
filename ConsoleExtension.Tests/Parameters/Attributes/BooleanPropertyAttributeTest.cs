namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Attributes
{
    using System.Linq;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class BooleanPropertyAttributeTest
    {
        private Mock<_PropertyInfo> mockPropertyInfo = new Mock<_PropertyInfo>();

        [TestCleanup]
        public void TestCleanup()
        {
            mockPropertyInfo.Reset();
        }

        [TestMethod]
        public void ValidateTest_TypeMismatch()
        {
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Recurse");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(string));

            var attribute = new BooleanPropertyAttribute("recurse", "r", "If new commits of all populated submodules should be fetched too.");
            var errors = attribute.Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopPropertyTypeMismatchError;
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Recurse", error.PropertyName);
            Assert.AreEqual("String", error.CurrentType);
            Assert.AreEqual("Boolean", error.SupportedTypes.Single());
        }
    }
}
