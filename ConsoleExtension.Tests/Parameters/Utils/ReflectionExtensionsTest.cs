namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Utils
{
    using System.Linq;
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

            var repositoryPropertyAttribute = propertyAttributes.First().Key;
            Assert.AreEqual("repository", repositoryPropertyAttribute.LongName);
            Assert.AreEqual("rep", repositoryPropertyAttribute.ShortName);
            Assert.IsFalse(string.IsNullOrWhiteSpace(repositoryPropertyAttribute.HelpMessage));
            Assert.IsTrue(repositoryPropertyAttribute.Required);

            var repositoryProperty = propertyAttributes.First().Value;
            Assert.AreEqual("Repository", repositoryProperty.Name);
            Assert.AreEqual(typeof(string), repositoryProperty.PropertyType);
        }

        [TestMethod]
        public void GetPropertyAttributes_HideProperty()
        {
            var propertyAttributes = typeof(DummyGitClone).GetPropertyAttributes();
            Assert.IsNotNull(propertyAttributes);
            Assert.AreEqual(1, propertyAttributes.Count);

            var repositoryPropertyAttribute = propertyAttributes.First().Key;
            Assert.AreEqual("repository", repositoryPropertyAttribute.LongName);
            Assert.AreEqual("rep", repositoryPropertyAttribute.ShortName);
            Assert.IsFalse(string.IsNullOrWhiteSpace(repositoryPropertyAttribute.HelpMessage));
            Assert.IsTrue(repositoryPropertyAttribute.Required);

            var repositoryProperty = propertyAttributes.First().Value;
            Assert.AreEqual("Repository", repositoryProperty.Name);
            Assert.AreEqual(typeof(string), repositoryProperty.PropertyType);
        }
    }
}
