namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Attributes
{
    using System.Linq;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class StringPropertyAttributeTest
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
            mockPropertyInfo.SetupGet(p => p.Name).Returns("Repository");
            mockPropertyInfo.SetupGet(p => p.CanWrite).Returns(true);
            mockPropertyInfo.SetupGet(p => p.PropertyType).Returns(typeof(bool));

            var attribute = new StringPropertyAttribute("repository", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.");
            var errors = attribute.Validate("Git", mockPropertyInfo.Object);
            Assert.AreEqual(1, errors.Count);
            var error = errors.First() as DevelopPropertyTypeMismatchError;
            Assert.AreEqual("Git", error.TypeName);
            Assert.AreEqual("Repository", error.PropertyName);
            Assert.AreEqual("Boolean", error.CurrentType);
            Assert.AreEqual("String", error.SupportedTypes.Single());
        }
    }
}
