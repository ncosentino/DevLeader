using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting.Hosting;

using IronPython.Hosting;

namespace DynamicScript
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter the text you would like the script to print!");
            var input = Console.ReadLine();

            var script =
                "class MyClass:\r\n" +
                "    def __init__(self):\r\n" +
                "        pass\r\n" +
                "    def go(self, input):\r\n" +
                "        print('From dynamic python: ' + input)\r\n" +
                "        return input";
           
            try
            {
                var engine = Python.CreateEngine();
                var scope = engine.CreateScope();
                var ops = engine.Operations;

                engine.Execute(script, scope);
                var pythonType = scope.GetVariable("MyClass");
                dynamic instance = ops.CreateInstance(pythonType);
                var value = instance.go(input);
                
                if (!input.Equals(value))
                {
                    throw new InvalidOperationException("Odd... The return value wasn't the same as what we input!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! There was an exception while running the script: " + ex.Message);
            }

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
