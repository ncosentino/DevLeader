using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ncosentino.ThreadPatternExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Press enter to start the single thread runner.");
            Console.ReadLine();
            Console.WriteLine("Press enter to stop the single thread runner.");

            var runner = SingleThreadRunner.Create();
            runner.Start();

            Console.ReadLine();
            runner.Stop();

            Console.WriteLine("Press enter to start the group thread runner.");
            Console.ReadLine();
            Console.WriteLine("Press enter to stop the group thread runner.");

            runner = GroupThreadRunner.Create();
            runner.Start();

            Console.ReadLine();
            runner.Stop();
        }
    }
}
