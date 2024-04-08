//
// You can read more about this topic here:
// https://www.devleader.ca/2024/03/29/collection-initializers-and-collection-expressions-in-csharp-code-examples/
// https://www.devleader.ca/2024/03/31/collection-initializer-performance-in-c-double-your-performance-with-what/
//
using BenchmarkDotNet.Running;

using System.Reflection;

BenchmarkRunner.Run(
    Assembly.GetExecutingAssembly(),
    args: args);
