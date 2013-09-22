using System;
using System.Collections.Generic;
using System.Text;

using IronPython.Hosting;

namespace RunExternalScript
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Press enter to execute the python script!");
            Console.ReadLine();

            var py = Python.CreateEngine();
            try
            {
                py.ExecuteFile("script.py");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! We couldn't execute the script because of an exception: " + ex.Message);
            }

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
