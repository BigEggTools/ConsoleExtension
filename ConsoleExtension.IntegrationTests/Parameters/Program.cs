namespace BigEgg.Tools.ConsoleExtension.IntegrationTests.Parameters
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;

    using BigEgg.Tools.ConsoleExtension.Parameters;

    using BigEgg.Tools.ConsoleExtension.IntegrationTests.Parameters.Params;

    public class Program
    {
        private static CompositionContainer container;
        private static AggregateCatalog catalog;
        private const int HEADER_LENGTH = 40;

        public static void Main(string[] args)
        {
            Initialize();

            RunTest("Empty Input", "", typeof(GitClone));
            RunTest("Duplicate Property", "--help --help", typeof(GitClone));
            RunTest("Version Request", "--version", typeof(GitClone));
            RunTest("TypeCheck Request", "--clone", typeof(GitClone), typeof(EmptyClass), typeof(InvalidCommandParam), typeof(InvalidPropertyParam));
            RunTest("Help Request", "--help", typeof(GitClone), typeof(GitPull));
            RunTest("Mising Command", "--repository http://abc.com", typeof(GitClone), typeof(GitPull));
            RunTest("Unknown Command", "error --repository http://abc.com", typeof(GitClone), typeof(GitPull));
            RunTest("Command Help Request", "clone --help", typeof(GitClone), typeof(GitPull));

            Console.WriteLine("All tests complete.");
            Console.ReadKey();
            Console.WriteLine("Bye.");
        }


        private static void Initialize()
        {
            catalog = new AggregateCatalog();
            // Add the Framework assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Parser).Assembly));
            // Add the Bugger.Presentation assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            container = new CompositionContainer(catalog);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);
        }

        private static void RunTest(string header, string arguments, params Type[] types)
        {
            OutputTestHeader($"Test {header}");
            Console.WriteLine($"app.exe {arguments}");

            var args = arguments.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var parameter = new Parser(container, ParserSettings.Builder().WithDefault().ComputeDisplayWidth().Build()).Parse(args, types);

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
