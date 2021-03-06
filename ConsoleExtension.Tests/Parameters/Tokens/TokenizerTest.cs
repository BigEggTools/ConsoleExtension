﻿namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Tokens
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Tokens;

    [TestClass]
    public class TokenizerTest : TestClassBase
    {
        [TestMethod]
        public void ToTokensTest_WithCommand()
        {
            var tokenizer = Container.GetExportedValue<ITokenizer>();
            var command = "clone --recursive --repository https://github.com/BigEggTools/JsonComparer.git --b master";
            var args = command.Split(' ');

            var tokens = tokenizer.ToTokens(args);
            Assert.AreEqual(4, tokens.Count);

            var cloneToken = tokens.FirstOrDefault(token => token.TokenType == TokenType.Command);
            Assert.IsNotNull(cloneToken);
            Assert.AreEqual("clone", cloneToken.Value);

            var recursiveToken = tokens.FirstOrDefault(token => token.TokenType == TokenType.Flag);
            Assert.IsNotNull(recursiveToken);
            Assert.AreEqual("recursive", recursiveToken.Name);

            var repositoryToken = tokens.FirstOrDefault(token => token.TokenType == TokenType.Property && token.Name.Equals("repository"));
            Assert.IsNotNull(repositoryToken);
            Assert.AreEqual("repository", repositoryToken.Name);
            Assert.AreEqual("https://github.com/BigEggTools/JsonComparer.git", repositoryToken.Value);

            var branchToken = tokens.FirstOrDefault(token => token.TokenType == TokenType.Property && token.Name.Equals("b"));
            Assert.IsNotNull(branchToken);
            Assert.AreEqual("b", branchToken.Name);
            Assert.AreEqual("master", branchToken.Value);
        }

        [TestMethod]
        public void ToTokensTest_NoCommand()
        {
            var tokenizer = Container.GetExportedValue<ITokenizer>();
            var command = "--recursive --repository https://github.com/BigEggTools/JsonComparer.git --b master";
            var args = command.Split(' ');

            var tokens = tokenizer.ToTokens(args);
            Assert.AreEqual(3, tokens.Count);
        }

        [TestMethod]
        public void ToTokensTest_Unknown()
        {
            var tokenizer = Container.GetExportedValue<ITokenizer>();
            var command = "clone unknown --repository https://github.com/BigEggTools/JsonComparer.git --b master";
            var args = command.Split(' ');

            var tokens = tokenizer.ToTokens(args);
            Assert.AreEqual(4, tokens.Count);

            var unknownToken = tokens.FirstOrDefault(token => token.TokenType == TokenType.Unknown);
            Assert.IsNotNull(unknownToken);
            Assert.AreEqual("unknown", unknownToken.Name);
        }

        [TestMethod]
        public void ToTokensTest_Version()
        {
            var tokenizer = Container.GetExportedValue<ITokenizer>();
            var command = "--version";
            var args = command.Split(' ');

            var tokens = tokenizer.ToTokens(args);
            Assert.AreEqual(1, tokens.Count);

            var versionToken = tokens.FirstOrDefault(token => token.TokenType == TokenType.Version);
            Assert.IsNotNull(versionToken);
        }

        [TestMethod]
        public void ToTokensTest_Help()
        {
            var tokenizer = Container.GetExportedValue<ITokenizer>();
            var command = "--help";
            var args = command.Split(' ');

            var tokens = tokenizer.ToTokens(args);
            Assert.AreEqual(1, tokens.Count);

            var helpToken = tokens.FirstOrDefault(token => token.TokenType == TokenType.Help);
            Assert.IsNotNull(helpToken);
        }

        [TestMethod]
        public void ToTokensTest_CommandHelp()
        {
            var tokenizer = Container.GetExportedValue<ITokenizer>();
            var command = "clone --help";
            var args = command.Split(' ');

            var tokens = tokenizer.ToTokens(args);
            Assert.AreEqual(2, tokens.Count);

            var cloneToken = tokens.FirstOrDefault(token => token.TokenType == TokenType.Command);
            Assert.IsNotNull(cloneToken);
            Assert.AreEqual("clone", cloneToken.Value);

            var helpToken = tokens.FirstOrDefault(token => token.TokenType == TokenType.Help);
            Assert.IsNotNull(helpToken);
        }

        [TestMethod]
        public void ToTokensTest_Duplicate()
        {
            var tokenizer = Container.GetExportedValue<ITokenizer>();
            var command = "clone --help --help";
            var args = command.Split(' ');

            var tokens = tokenizer.ToTokens(args);
            Assert.AreEqual(3, tokens.Count);
        }
    }
}
