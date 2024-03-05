using BenchmarkDotNet.Attributes;

namespace Benchmarking.BenchmarkDotNet.BenchmarkBaseClass;

[ShortRunJob]
public class Benchmarks
{
    [Benchmark]
    public void This_Is_A_Test_Benchmark()
    {
        for (int i = 0; i < 10_000; i++)
        {
            Random.Shared.Next();
        }
    }
}
