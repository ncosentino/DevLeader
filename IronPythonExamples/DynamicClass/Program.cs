using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.CSharp.RuntimeBinder;

using IronPython.Hosting;

namespace DynamicClass
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Press enter to read the value of 'MyProperty' from a Python object before we actually add the dynamic property.");
            Console.ReadLine();

            // this script was taken from this blog post:
            // http://znasibov.info/blog/html/2010/03/10/python-classes-dynamic-properties.html
            var script =
                "class Properties(object):\r\n" +
                "    def add_property(self, name, value):\r\n" +
                "        # create local fget and fset functions\r\n" +
                "        fget = lambda self: self._get_property(name)\r\n" +
                "        fset = lambda self, value: self._set_property(name, value)\r\n" +
                "\r\n" +
                "        # add property to self\r\n" +
                "        setattr(self.__class__, name, property(fget, fset))\r\n" +
                "        # add corresponding local variable\r\n" +
                "        setattr(self, '_' + name, value)\r\n" +
                "\r\n" +
                "\r\n" +
                "    def _set_property(self, name, value):\r\n" +
                "        setattr(self, '_' + name, value)\r\n" +
                "\r\n" +
                "    def _get_property(self, name):\r\n" +
                "        return getattr(self, '_' + name)\r\n";

            try
            {
                var engine = Python.CreateEngine();
                var scope = engine.CreateScope();
                var ops = engine.Operations;

                engine.Execute(script, scope);
                var pythonType = scope.GetVariable("Properties");
                dynamic instance = ops.CreateInstance(pythonType);

                try
                {
                    Console.WriteLine(instance.MyProperty);
                    throw new InvalidOperationException("This class doesn't have the property we want, so this should be impossible!");
                }
                catch (RuntimeBinderException)
                {
                    Console.WriteLine("We got the exception as expected!");
                }

                Console.WriteLine();
                Console.WriteLine("Press enter to add the property 'MyProperty' to our Python object and then try to read the value.");
                Console.ReadLine();

                instance.add_property("MyProperty", "Expected value of MyProperty!");
                Console.WriteLine(instance.MyProperty);
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
