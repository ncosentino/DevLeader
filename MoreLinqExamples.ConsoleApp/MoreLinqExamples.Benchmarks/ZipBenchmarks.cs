// Watch videos on YouTube:
// - 

using BenchmarkDotNet.Attributes;

using MoreLinq;

[MemoryDiagnoser]
[ShortRunJob]
public class ZipBenchmarks
{
    private int[]? _sourceArray1;
    private int[]? _sourceArray2;
    private IEnumerable<int>? _sourceEnumerable1;
    private IEnumerable<int>? _sourceEnumerable2;

    [Params(10, 1_000, 1_000_000, 10_000_000)]
    public int CollectionSize1;

    [Params(10, 1_000, 1_000_000, 10_000_000)]
    public int CollectionSize2;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _sourceArray1 = Enumerable.Range(1, CollectionSize1).ToArray();
        _sourceArray2 = Enumerable.Range(1, CollectionSize2).ToArray();
        _sourceEnumerable1 = Enumerable.Range(1, CollectionSize1);
        _sourceEnumerable2 = Enumerable.Range(1, CollectionSize2);
    }

    [Benchmark]
    public void Array_MoreLinqZipShortest()
    {
        foreach (var entry in _sourceArray1!.ZipShortest(_sourceArray2!, (x, y) => (x, y)))
        {
        }
    }

    [Benchmark]
    public void Array_MoreLinqZipLongest()
    {
        foreach (var entry in _sourceArray1!.ZipLongest(_sourceArray2!, (x, y) => (x, y)))
        {
        }
    }

    [Benchmark(Baseline = true)]
    public void Array_LinqZip()
    {
        foreach (var entry in _sourceArray1!.Zip(_sourceArray2!, (x, y) => (x, y)))
        {
        }
    }

    [Benchmark]
    public void Enumerable_MoreLinqZipShortest()
    {
        foreach (var entry in _sourceEnumerable1!.ZipShortest(_sourceEnumerable2!, (x, y) => (x, y)))
        {
        }
    }

    [Benchmark]
    public void Enumerable_MoreLinqZipLongest()
    {
        foreach (var entry in _sourceEnumerable1!.ZipLongest(_sourceEnumerable2!, (x, y) => (x, y)))
        {
        }
    }

    [Benchmark]
    public void Enumerable_LinqZip()
    {
        foreach (var entry in _sourceEnumerable1!.Zip(_sourceEnumerable2!, (x, y) => (x, y)))
        {
        }
    }

    [Benchmark]
    public void Enumerable_ManualZipShortest()
    {
        foreach (var entry in _sourceEnumerable1!.ManualZipShortest(_sourceEnumerable2!, (x, y) => (x, y)))
        {
        }
    }

    [Benchmark]
    public void Array_ManualZipShortest()
    {
        foreach (var entry in _sourceArray1!.ManualZipShortest(_sourceArray2!, (x, y) => (x, y)))
        {
        }
    }
}