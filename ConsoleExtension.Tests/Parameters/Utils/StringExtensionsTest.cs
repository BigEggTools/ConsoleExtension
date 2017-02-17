namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Utils
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.Parameters.Utils;

    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void FormatWithIndex_NoIndex()
        {
            var text = "Program name: ConsoleExtension".FormatWithIndex(80);

            Assert.AreEqual($"Program name: ConsoleExtension{Environment.NewLine}", text);
        }

        [TestMethod]
        public void FormatWithIndex_NoIndex_ShouldNotWrap()
        {
            var text = "Program name: ConsoleExtension".FormatWithIndex(25);

            Assert.AreEqual($"Program name: ConsoleExtension{Environment.NewLine}", text);
        }

        [TestMethod]
        public void FormatWithIndex_NoIndex_MultipleLine()
        {
            var text = new string[]
            {
                "Program Version Information:",
                "Program Name: ConsoleExtension",
                "Program Version: 1.0.0",
                "Program Product: BigEgg.Tools",
                "Program Copyright: Copyright c BigEgg 2017",
            }.FormatWithIndex(80);

            Assert.AreEqual($"Program Version Information:{Environment.NewLine}Program Name: ConsoleExtension{Environment.NewLine}Program Version: 1.0.0{Environment.NewLine}Program Product: BigEgg.Tools{Environment.NewLine}Program Copyright: Copyright c BigEgg 2017{Environment.NewLine}", text);
        }

        [TestMethod]
        public void FormatWithIndex_WithIndexTest()
        {
            var text = $"Program name: {ParameterConstants.INDEX_START_STRING}ConsoleExtension".FormatWithIndex(80);

            Assert.AreEqual($"Program name: ConsoleExtension{Environment.NewLine}", text);
        }

        [TestMethod]
        public void FormatWithIndex_WithIndexTest_Wrap()
        {
            var text = $"Program name: {ParameterConstants.INDEX_START_STRING}ConsoleExtension".FormatWithIndex(25);

            Assert.AreEqual($"Program name: ConsoleExte{new string(' ', 14)}nsion{Environment.NewLine}", text);
        }

        [TestMethod]
        public void FormatWithIndex_WithIndexTest_MultipleLine()
        {
            var text = new string[]
            {
                "Program Version Information:",
                $"Program Name: {ParameterConstants.INDEX_START_STRING}ConsoleExtension",
                $"Program Version: {ParameterConstants.INDEX_START_STRING}1.0.0",
                $"Program Product: {ParameterConstants.INDEX_START_STRING}BigEgg.Tools",
                $"Program Copyright: {ParameterConstants.INDEX_START_STRING}Copyright c 2017 BigEgg"
            }.FormatWithIndex(80);

            Assert.AreEqual($"Program Version Information:{Environment.NewLine}Program Name: ConsoleExtension{Environment.NewLine}Program Version: 1.0.0{Environment.NewLine}Program Product: BigEgg.Tools{Environment.NewLine}Program Copyright: Copyright c 2017 BigEgg{Environment.NewLine}", text);
        }

        [TestMethod]
        public void FormatWithIndex_WithIndexTest_MultipleLineWithWrap()
        {
            var text = new string[]
            {
                "Program Version Information:",
                $"Program Name: {ParameterConstants.INDEX_START_STRING}ConsoleExtension",
                $"Program Version: {ParameterConstants.INDEX_START_STRING}1.0.0",
                $"Program Product: {ParameterConstants.INDEX_START_STRING}BigEgg.Tools",
                $"Program Copyright: {ParameterConstants.INDEX_START_STRING}Copyright c 2017 BigEgg"
            }.FormatWithIndex(25);

            Assert.AreEqual($"Program Version Information:{Environment.NewLine}Program Name: ConsoleExte{new string(' ', 14)}nsion{Environment.NewLine}Program Version: 1.0.0{Environment.NewLine}Program Product: BigEgg.T{new string(' ', 17)}ools{Environment.NewLine}Program Copyright: Copyri{new string(' ', 19)}ght c {new string(' ', 19)}2017 B{new string(' ', 19)}igEgg{Environment.NewLine}", text);
        }
    }
}
