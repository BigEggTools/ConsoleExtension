using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BigEgg.Tools.ConsoleExtension.Tests.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.ConsoleExtension.Parameters.Output;

    [TestClass]
    public class OutputFormatBaseTest
    {
        [TestClass]
        public class FormatTest_NoIndex
        {
            private readonly OutputFormat output = new OutputFormat();

            [TestMethod]
            public void Replace()
            {
                var text = output.Format(new string[] { "Program name: |name|" }, new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("|name|", "ConsoleExtension")
                }, 80);

                Assert.AreEqual($"Program name: ConsoleExtension{Environment.NewLine}", text);
            }

            [TestMethod]
            public void MultiReplace()
            {
                var text = output.Format(new string[] { "|name|: v|version|" }, new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("|name|", "ConsoleExtension"),
                    new Tuple<string, string>("|version|", "1.0.0")
                }, 80);

                Assert.AreEqual($"ConsoleExtension: v1.0.0{Environment.NewLine}", text);
            }

            [TestMethod]
            public void Wrap()
            {
                var text = output.Format(new string[] { "Program name: |name|" }, new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("|name|", "ConsoleExtension")
                }, 25);

                Assert.AreEqual($"Program name: ConsoleExte{Environment.NewLine}nsion{Environment.NewLine}", text);
            }

            [TestMethod]
            public void NoReplace()
            {
                var text = output.Format(new string[] { "Program Version Information:" }, new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("|name|", "ConsoleExtension")
                }, 80);

                Assert.AreEqual($"Program Version Information:{Environment.NewLine}", text);
            }

            [TestMethod]
            public void MultipleLine()
            {
                var text = output.Format(
                    new string[]
                    {
                        "Program Version Information:",
                        "Program Name: |name|",
                        "Program Version: |version|",
                        "Program Product: |product|",
                        "Program Copyright: |copyright|",
                    },
                    new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>("|name|", "ConsoleExtension"),
                        new Tuple<string, string>("|version|", "1.0.0"),
                        new Tuple<string, string>("|product|", "BigEgg.Tools"),
                        new Tuple<string, string>("|copyright|", "Copyright c BigEgg 2017")
                    }, 80);

                Assert.AreEqual($"Program Version Information:{Environment.NewLine}Program Name: ConsoleExtension{Environment.NewLine}Program Version: 1.0.0{Environment.NewLine}Program Product: BigEgg.Tools{Environment.NewLine}Program Copyright: Copyright c BigEgg 2017{Environment.NewLine}", text);
            }
        }

        [TestClass]
        public class WithIndexTest
        {
            private readonly OutputFormat output = new OutputFormat();

            [TestMethod]
            public void Replace()
            {
                var text = output.Format(new string[] { $"Program name: {output.INDEX_START_STRING}|name|" }, new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("|name|", "ConsoleExtension")
                }, 80);

                Assert.AreEqual($"Program name: ConsoleExtension{Environment.NewLine}", text);
            }

            [TestMethod]
            public void MultiReplace()
            {
                var text = output.Format(new string[] { $"|name|: {output.INDEX_START_STRING}v|version|" }, new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("|name|", "ConsoleExtension"),
                    new Tuple<string, string>("|version|", "1.0.0")
                }, 80);

                Assert.AreEqual($"ConsoleExtension: v1.0.0{Environment.NewLine}", text);
            }

            [TestMethod]
            public void Wrap()
            {
                var text = output.Format(new string[] { $"Program name: {output.INDEX_START_STRING}|name|" }, new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("|name|", "ConsoleExtension")
                }, 25);

                Assert.AreEqual($"Program name: ConsoleExte{Environment.NewLine}{new string(' ', 14)}nsion{Environment.NewLine}", text);
            }

            [TestMethod]
            public void NoReplace()
            {
                var text = output.Format(new string[] { $"Program Version Information:{output.INDEX_START_STRING}" }, new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("|name|", "ConsoleExtension")
                }, 80);

                Assert.AreEqual($"Program Version Information:{Environment.NewLine}", text);
            }

            [TestMethod]
            public void MultipleLine()
            {
                var text = output.Format(
                    new string[]
                    {
                        "Program Version Information:",
                        $"Program Name: {output.INDEX_START_STRING}|name|",
                        $"Program Version: {output.INDEX_START_STRING}|version|",
                        $"Program Product: {output.INDEX_START_STRING}|product|",
                        $"Program Copyright: {output.INDEX_START_STRING}|copyright|",
                    },
                    new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>("|name|", "ConsoleExtension"),
                        new Tuple<string, string>("|version|", "1.0.0"),
                        new Tuple<string, string>("|product|", "BigEgg.Tools"),
                        new Tuple<string, string>("|copyright|", "Copyright c BigEgg 2017")
                    }, 80);

                Assert.AreEqual($"Program Version Information:{Environment.NewLine}Program Name: ConsoleExtension{Environment.NewLine}Program Version: 1.0.0{Environment.NewLine}Program Product: BigEgg.Tools{Environment.NewLine}Program Copyright: Copyright c BigEgg 2017{Environment.NewLine}", text);
            }

            [TestMethod]
            public void MultipleLineWithWrap()
            {
                var text = output.Format(
                    new string[]
                    {
                        "Program Version Information:",
                        $"Program Name: {output.INDEX_START_STRING}|name|",
                        $"Program Version: {output.INDEX_START_STRING}|version|",
                        $"Program Product: {output.INDEX_START_STRING}|product|",
                        $"Program Copyright: {output.INDEX_START_STRING}|copyright|",
                    },
                    new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>("|name|", "ConsoleExtension"),
                        new Tuple<string, string>("|version|", "1.0.0"),
                        new Tuple<string, string>("|product|", "BigEgg.Tools"),
                        new Tuple<string, string>("|copyright|", "Copyright c BigEgg 2017")
                    }, 25);

                Assert.AreEqual($"Program Version Informati{Environment.NewLine}on:{Environment.NewLine}Program Name: ConsoleExte{Environment.NewLine}{new string(' ', 14)}nsion{Environment.NewLine}Program Version: 1.0.0{Environment.NewLine}Program Product: BigEgg.T{Environment.NewLine}{new string(' ', 17)}ools{Environment.NewLine}Program Copyright: Copyri{Environment.NewLine}{new string(' ', 19)}ght c {Environment.NewLine}{new string(' ', 19)}BigEgg{Environment.NewLine}{new string(' ', 19)} 2017{Environment.NewLine}", text);
            }
        }


        internal class OutputFormat : OutputFormatBase
        {
            public new string INDEX_START_STRING { get { return base.INDEX_START_STRING; } }

            public new string Format(string[] formatString, IList<Tuple<string, string>> args, int maximumDisplayWidth)
            {
                return base.Format(formatString, args, maximumDisplayWidth);
            }
        }
    }
}
