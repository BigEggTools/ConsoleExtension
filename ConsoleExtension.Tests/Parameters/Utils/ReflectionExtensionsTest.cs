namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Utils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    using BigEgg.Tools.ConsoleExtension.Tests.Parameters.FakeParameters;

    [TestClass]
    public class ReflectionExtensionsTest
    {
        [TestMethod]
        public void GetCommandAttribute_NotExist()
        {
            var commandAttribute = typeof(EmptyClass).GetCommandAttributes();
            Assert.IsNull(commandAttribute);
        }

        [TestMethod]
        public void GetCommandAttribute_Exist()
        {
            var commandAttribute = typeof(GitClone).GetCommandAttributes();
            Assert.IsNotNull(commandAttribute);
            Assert.AreEqual("Clone", commandAttribute.Name);
            Assert.IsFalse(string.IsNullOrWhiteSpace(commandAttribute.HelpMessage));
        }

        [TestMethod]
        public void GetPropertyAttributes_NostExist()
        {
            var propertyAttributes = typeof(EmptyClass).GetPropertyAttributes();
            Assert.IsNotNull(propertyAttributes);
            Assert.AreEqual(0, propertyAttributes.Count);
        }

        [TestMethod]
        public void GetPropertyAttributes_Exist()
        {
            var propertyAttributes = typeof(GitClone).GetPropertyAttributes();
            Assert.IsNotNull(propertyAttributes);
            Assert.AreEqual(1, propertyAttributes.Count);

            var repositoryProperty = propertyAttributes[0];
            Assert.AreEqual("repository", repositoryProperty.LongName);
            Assert.AreEqual("rep", repositoryProperty.ShortName);
            Assert.IsFalse(string.IsNullOrWhiteSpace(repositoryProperty.HelpMessage));
            Assert.IsTrue(repositoryProperty.Required);
        }
    }
}
