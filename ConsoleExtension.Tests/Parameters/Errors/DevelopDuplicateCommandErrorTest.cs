namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Errors
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class DevelopDuplicateCommandErrorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new DevelopDuplicateCommandError("GitClone", "GitPush", "GitClone");
            Assert.AreEqual(ErrorType.Develop_DuplicateCommand, error.ErrorType);
            Assert.AreEqual("GitClone", error.CommandName);
            Assert.AreEqual("GitPush", error.TypeName1);
            Assert.AreEqual("GitClone", error.TypeName2);
            Assert.IsTrue(error.StopProcessing);
        }
    }
}
