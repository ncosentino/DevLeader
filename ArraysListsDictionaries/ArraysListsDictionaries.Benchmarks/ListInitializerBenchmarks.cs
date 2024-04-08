//
// You can read more about this topic here:
// https://www.devleader.ca/2024/03/29/collection-initializers-and-collection-expressions-in-csharp-code-examples/
// https://www.devleader.ca/2024/03/31/collection-initializer-performance-in-c-double-your-performance-with-what/
//
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[MediumRunJob]
public class ListInitializerBenchmarks
{
    private static readonly string[] _dataAsArray = new string[]
    {
        "Apple",
        "Banana",
        "Orange",
    };

    private static IEnumerable<string> GetDataAsIterator()
    {
        yield return "Apple";
        yield return "Banana";
        yield return "Orange";
    }

    [Benchmark(Baseline = true)]
    public List<string> ClassicCollectionInitializer_NoCapacity()
    {
        return new List<string>()
        {
            "Apple",
            "Banana",
            "Orange",
        };
    }

    [Benchmark]
    public List<string> ClassicCollectionInitializer_SetCapacity()
    {
        return new List<string>(3)
        {
            "Apple",
            "Banana",
            "Orange",
        };
    }

    [Benchmark]
    public List<string> CollectionExpression()
    {
        return
        [
            "Apple",
            "Banana",
            "Orange",
        ];
    }

    [Benchmark]
    public List<string> CopyConstructor_Array()
    {
        return new List<string>(_dataAsArray);
    }

    [Benchmark]
    public List<string> CopyConstructor_Iterator()
    {
        return new List<string>(GetDataAsIterator());
    }

    [Benchmark]
    public List<string> ManuallyAdd_NoCapacitySet()
    {
        List<string> list = [];
        list.Add("Apple");
        list.Add("Banana");
        list.Add("Orange");
        return list;
    }

    [Benchmark]
    public List<string> ManuallyAdd_CapacitySet()
    {
        List<string> list = new(3);
        list.Add("Apple");
        list.Add("Banana");
        list.Add("Orange");
        return list;
    }
}
