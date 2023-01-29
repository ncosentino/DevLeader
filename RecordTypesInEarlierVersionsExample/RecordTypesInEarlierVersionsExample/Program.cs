using System;
using System.Reflection;

/// <Summary>
/// This code is discussed on this blog post:
/// https://www.devleader.ca/2023/01/29/simple-secrets-to-access-to-the-dotnet-record-type/
/// </Summary>
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
