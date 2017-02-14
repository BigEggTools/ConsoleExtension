namespace BigEgg.Tools.ConsoleExtension.IntegrationTests.Parameters
{
    using System;

    using BigEgg.Tools.ConsoleExtension.Parameters;
    using BigEgg.Tools.ConsoleExtension.IntegrationTests.Parameters.Params;

    public partial class Program : ProgramBase
    {
        private const int HEADER_LENGTH = 40;

        public static void Main(string[] args)
        {
            Initialize();

            RunTest("Empty Input", "", typeof(GitClone));
            RunTest("Duplicate Property", "--help --help", typeof(GitClone));
            RunTest("Version Request", "--version", typeof(GitClone));
            RunTest("TypeCheck Request", "--clone", typeof(GitClone), typeof(EmptyClass), typeof(InvalidCommandParam), typeof(InvalidPropertyParam));

            Console.WriteLine("All test complete.");
            Console.ReadKey();
            Console.WriteLine("Bye.");
        }


        private static void RunTest(string header, string arguments, params Type[] types)
        {
            OutputTestHeader($"Test {header}");
            Console.WriteLine($"app.exe {arguments}");

            var args = arguments.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var parameter = new Parser(container).Parse(args, types);

            Console.WriteLine();
            Console.ReadKey();
        }

        private static void OutputTestHeader(string header)
        {
            var emptyLength = HEADER_LENGTH - header.Length;
            var prefixLength = emptyLength / 2;
            var postLength = emptyLength - prefixLength;

            var output = new string('=', prefixLength) + header + new string('=', postLength);

            Console.WriteLine(output);
        }
    }
}
