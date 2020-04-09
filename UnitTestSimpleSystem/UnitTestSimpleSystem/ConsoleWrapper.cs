using System;

namespace UnitTestSimpleSystem
{
    public sealed class ConsoleWrapper : IConsole
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
