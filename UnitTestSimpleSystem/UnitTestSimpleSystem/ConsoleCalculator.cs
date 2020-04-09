using System;

namespace UnitTestSimpleSystem
{
    public sealed class ConsoleCalculator
    {
        private readonly IConsole _console;

        public ConsoleCalculator(IConsole console)
        {
            _console = console;
        }

        public int Add(int num1, int num2)
        {
            var result = num1 + num2;
            _console.WriteLine($"{num1}+{num2}={result}");

            return result;
        }
    }
}
