using System.Reflection;

using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkSwitcher
    .FromAssembly(Assembly.GetExecutingAssembly())
    .Run(args);

[MemoryDiagnoser]
[ShortRunJob]
public class OurBenchmarks
{
    List<int>? _list;

    [Params(1_000, 10_000, 100_000, 1_000_000)]
    public int ListSize;

    [GlobalSetup]
    public void Setup()
    {
        _list = new List<int>();
        for (int i = 0; i < ListSize; i++)
        {
            _list.Add(i);
        }
    }

    [Benchmark]
    public void OurBenchmark()
    {
        _list!.Sort();
    }
}