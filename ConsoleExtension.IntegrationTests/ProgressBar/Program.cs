using BigEgg.Tools.ConsoleExtension.ProgressBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigEgg.Tools.ConsoleExtension.IntegrationTests.ProgressBar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                TextProgressBar.Draw(i, 10);
                Thread.Sleep(500);
            }
            TextProgressBar.Draw(10, 10);

            Console.WriteLine("All tests complete.");
            Console.ReadKey();
            Console.WriteLine("Bye.");
        }
    }
}
