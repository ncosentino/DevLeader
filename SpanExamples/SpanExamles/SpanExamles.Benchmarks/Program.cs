using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

using SpanExamles.Benchmarks;

using System.Reflection;

var config = ManualConfig
    .Create(DefaultConfig.Instance);
var summary = BenchmarkSwitcher
    .FromAssembly(Assembly.GetExecutingAssembly())
    .Run(args, config);

var noCaseBenchmarks = summary
    .Where(x => !x.BenchmarksCases.Any())
    .ToArray();
if (noCaseBenchmarks.Any())
{
    throw new InvalidOperationException("Some benchmarks had no results");
}