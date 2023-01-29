using System;
using System.Reflection;

namespace RecordTypesInEarlierVersionsExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Assembly Runtime: {Assembly.GetExecutingAssembly().ImageRuntimeVersion}");

            var myRecord = new MyData(
                "Hello world!",
                123);
            Console.WriteLine(myRecord);            
        }
    }

    internal sealed record MyData(
        string Value1,
        int Value2);
}