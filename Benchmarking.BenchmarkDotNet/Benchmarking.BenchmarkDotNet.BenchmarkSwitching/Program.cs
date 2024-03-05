//
// Read more about this at:
// https://www.devleader.ca/2024/03/05/how-to-use-benchmarkdotnet-simple-performance-boosting-tips-to-get-started/
//
using BenchmarkDotNet.Running;

var assembly = typeof(Benchmarking.BenchmarkDotNet.BenchmarkBaseClass.Benchmarks).Assembly;


BenchmarkSwitcher
    // used to load all benchmarks from an assembly
    .FromAssembly(assembly)
    // OR... if there are multiple assemblies,
    // you can use this instead
    //.FromAssemblies
    // OR... if you'd rather specify the benchmark
    // types directly, you can use this
    ///.FromTypes(new[]
    ///{
    ///    typeof(MyBenchmark1),
    ///    typeof(MyBenchmark2),
    ///})
    .Run(args);