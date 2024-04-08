//
// You can read more about this topic here:
// https://www.devleader.ca/2024/03/29/collection-initializers-and-collection-expressions-in-csharp-code-examples/
// https://www.devleader.ca/2024/03/31/collection-initializer-performance-in-c-double-your-performance-with-what/
//
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[MediumRunJob]
public class DictionaryInitializerBenchmarks
{
    private static readonly Dictionary<string, string> _sourceData = new()
    {
        ["Apple"] = "The first value",
        ["Banana"] = "The next value",
        ["Orange"] = "The last value",
    };

    private static IEnumerable<KeyValuePair<string, string>> GetDataAsIterator()
    {
        foreach (var item in _sourceData)
        {
            yield return item;
        }
    }

    [Benchmark(Baseline = true)]
    public Dictionary<string, string> CollectionInitializer_BracesWithoutCapacity()
    {
        return new Dictionary<string, string>()
        {
            { "Apple", "The first value" },
            { "Banana", "The next value" },
            { "Orange",  "The last value" },
        };
    }

    [Benchmark]
    public Dictionary<string, string> CollectionInitializer_BracesWithCapacity()
    {
        return new Dictionary<string, string>(3)
        {
            { "Apple", "The first value" },
            { "Banana", "The next value" },
            { "Orange",  "The last value" },
        };
    }

    [Benchmark]
    public Dictionary<string, string> CollectionInitializer_BracketsWithoutCapacity()
    {
        return new Dictionary<string, string>()
        {
            ["Apple"] = "The first value",
            ["Banana"] = "The next value",
            ["Orange"] = "The last value",
        };
    }

    [Benchmark]
    public Dictionary<string, string> CollectionInitializer_BracketsWithCapacity()
    {
        return new Dictionary<string, string>(3)
        {
            ["Apple"] = "The first value",
            ["Banana"] = "The next value",
            ["Orange"] = "The last value",
        };
    }

    [Benchmark]
    public Dictionary<string, string> CopyConstructor_Dictionary()
    {
        return new Dictionary<string, string>(_sourceData);
    }

    [Benchmark]
    public Dictionary<string, string> CopyConstructor_Iterator()
    {
        return new Dictionary<string, string>(GetDataAsIterator());
    }

    [Benchmark]
    public Dictionary<string, string> ManuallyAdd_NoCapacitySet()
    {
        Dictionary<string, string> dict = [];
        dict.Add("Apple", "The first value");
        dict.Add("Banana", "The next value");
        dict.Add("Orange", "The last value");
        return dict;
    }

    [Benchmark]
    public Dictionary<string, string> ManuallyAdd_NoCapacitySet_UseNew()
    {
        Dictionary<string, string> dict = new();
        dict.Add("Apple", "The first value");
        dict.Add("Banana", "The next value");
        dict.Add("Orange", "The last value");
        return dict;
    }

    [Benchmark]
    public Dictionary<string, string> ManuallyAdd_CapacitySet()
    {
        Dictionary<string, string> dict = new(3);
        dict.Add("Apple", "The first value");
        dict.Add("Banana", "The next value");
        dict.Add("Orange", "The last value");
        return dict;
    }

    [Benchmark]
    public Dictionary<string, string> ManuallyAssign_NoCapacitySet()
    {
        Dictionary<string, string> dict = [];
        dict["Apple"] = "The first value";
        dict["Banana"] = "The next value";
        dict["Orange"] = "The last value";
        return dict;
    }

    [Benchmark]
    public Dictionary<string, string> ManuallyAssign_NoCapacitySet_UseNew()
    {
        Dictionary<string, string> dict = new();
        dict["Apple"] = "The first value";
        dict["Banana"] = "The next value";
        dict["Orange"] = "The last value";
        return dict;
    }

    [Benchmark]
    public Dictionary<string, string> ManuallyAssign_CapacitySet()
    {
        Dictionary<string, string> dict = new(3);
        dict["Apple"] = "The first value";
        dict["Banana"] = "The next value";
        dict["Orange"] = "The last value";
        return dict;
    }
}