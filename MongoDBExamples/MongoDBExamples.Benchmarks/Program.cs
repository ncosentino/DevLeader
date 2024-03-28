using BenchmarkDotNet.Running;

using System.Reflection;

BenchmarkRunner.Run(
    Assembly.GetExecutingAssembly(),
    args: args);