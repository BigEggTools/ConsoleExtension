namespace BigEgg.Tools.ConsoleExtension.IntegrationTests.Parameters
{
    using System;
    using System.Collections.Generic;

    using BigEgg.Tools.ConsoleExtension.Parameters;

    public class DuplicateProperty : ProgramBase
    {
        public static void Main(string[] args)
        {
            Initialize();

            var arguments = new List<string>();
            arguments.Add("--help");
            arguments.Add("--help");
            var parameter = new Parser(container).Parse(arguments, typeof(GitClone));

            Console.ReadKey();
        }
    }
}
