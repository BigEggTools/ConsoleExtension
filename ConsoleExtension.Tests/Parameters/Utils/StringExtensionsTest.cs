namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Utils
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void JoinNewLineTest()
        {
            var message1 = $"  asdf1234{Environment.NewLine}asdf1234";
            var message2 = $"  asdf1234{Environment.NewLine}asdf1234{Environment.NewLine}";
            var message3 = $"";
            var message4 = $"  asdf1234";

            var messages = new List<string>() { message1, message2, message3, message4 };
            var result = messages.Join();

            var expect = string.Join(Environment.NewLine, new List<string>() {
                "  asdf1234",
                "asdf1234",
                "  asdf1234",
                "asdf1234",
                "",
                "  asdf1234",
                ""
            });
            Assert.AreEqual(expect, result);
        }
    }
}
