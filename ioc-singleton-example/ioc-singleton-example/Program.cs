using System;
using System.Collections.Generic;
using System.Text;

using ApplicationLayer;

namespace ioc_singleton_example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.QueryType += (sender, e) =>
            {
                e.Type = typeof(ApplicationB);
            };

            Console.WriteLine(string.Format(
                "Application Name: {0}\r\nVersion: {1}",
                Application.Instance.Name,
                Application.Instance.Version));

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }

    internal class ApplicationA : IApplication
    {
        private ApplicationA()
        {
        }

        public string Name
        {
            get
            {
                return "Application A (Pretend this was from the assembly info)";
            }
        }

        public string Version
        {
            get { return "1234"; }
        }
    }

    internal class ApplicationB : IApplication
    {
        private ApplicationB()
        {
        }

        public string Name
        {
            get
            {
                return "Application B (Pretend this was from an XML file)";
            }
        }

        public string Version
        {
            get { return "9876"; }
        }
    }
}
