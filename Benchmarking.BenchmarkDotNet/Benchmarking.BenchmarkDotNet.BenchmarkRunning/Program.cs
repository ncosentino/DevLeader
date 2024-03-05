//
// Read more about this at:
// https://www.devleader.ca/2024/03/05/how-to-use-benchmarkdotnet-simple-performance-boosting-tips-to-get-started/
//
using BenchmarkDotNet.Running;

var assembly = typeof(Benchmarking.BenchmarkDotNet.BenchmarkBaseClass.Benchmarks).Assembly;

BenchmarkRunner.Run(
    assembly,
    args: args);