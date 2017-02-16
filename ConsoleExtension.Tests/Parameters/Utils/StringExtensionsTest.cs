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

            Assert.AreEqual($"Program name: ConsoleExt{Environment.NewLine}{new string(' ', 14)}ension{Environment.NewLine}", text);
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
                $"Program Copyright: {ParameterConstants.INDEX_START_STRING}Copyright c BigEgg 2017"
            }.FormatWithIndex(80);

            Assert.AreEqual($"Program Version Information:{Environment.NewLine}Program Name: ConsoleExtension{Environment.NewLine}Program Version: 1.0.0{Environment.NewLine}Program Product: BigEgg.Tools{Environment.NewLine}Program Copyright: Copyright c BigEgg 2017{Environment.NewLine}", text);
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
                $"Program Copyright: {ParameterConstants.INDEX_START_STRING}Copyright c BigEgg 2017"
            }.FormatWithIndex(25);

            Assert.AreEqual($"Program Version Information:{Environment.NewLine}Program Name: ConsoleExt{Environment.NewLine}{new string(' ', 14)}ension{Environment.NewLine}Program Version: 1.0.0{Environment.NewLine}Program Product: BigEgg.{Environment.NewLine}{new string(' ', 17)}Tools{Environment.NewLine}Program Copyright: Copyr{Environment.NewLine}{new string(' ', 19)}ight {Environment.NewLine}{new string(' ', 19)}c Big{Environment.NewLine}{new string(' ', 19)}Egg 2{Environment.NewLine}{new string(' ', 19)}017{Environment.NewLine}", text);
        }
    }
}
