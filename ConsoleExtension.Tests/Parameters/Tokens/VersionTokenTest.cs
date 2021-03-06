﻿namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Tokens
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;

    [TestClass]
    public class VersionTokenTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var token = new VersionToken();
            Assert.AreEqual(ParameterConstants.TOKEN_VERSION_NAME, token.Name);
            Assert.AreEqual(TokenType.Version, token.TokenType);
            Assert.IsTrue(string.IsNullOrWhiteSpace(token.Value));
        }
    }
}
