//
// If you want to follow along with content about these benchmarks, you can
// check out the information here:
// https://www.devleader.ca/2024/03/28/c-mongodb-insert-benchmarks-what-you-need-to-know/
//
using BenchmarkDotNet.Running;

using System.Reflection;

BenchmarkRunner.Run(
    Assembly.GetExecutingAssembly(),
    args: args);
