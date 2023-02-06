using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

var config = ManualConfig
    .Create(DefaultConfig.Instance)
    .WithOptions(ConfigOptions.DisableOptimizationsValidator);

var summary = BenchmarkRunner.Run(
    typeof(Benchmarks),
    config);

if (!summary.BenchmarksCases.Any())
{
    throw new InvalidOperationException("Benchmarks did not have results.");
}

[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.Method)]
[ShortRunJob(RuntimeMoniker.Net70)]
[MemoryDiagnoser]
public class Benchmarks
{
    public enum ImplementationScenario
    {
        Iterator,
        PopulateAsList,
        PopulateAsROCollection
    }

    private int[] _dataset;

    [Params(
        1_000,
        100_000,
        10_000_000)]
    public int NumberOfEntriesInDataset;

    [Params(
        ImplementationScenario.Iterator,
        ImplementationScenario.PopulateAsList, 
        ImplementationScenario.PopulateAsROCollection)]
    public ImplementationScenario Implementation;

    [GlobalSetup]
    public void Setup()
    {
        // seed this for consistency
        var random = new Random(1337);
        _dataset = new int[NumberOfEntriesInDataset];
        for (int i = 0; i < _dataset.Length; i++)
        {
            _dataset[i] = random.Next();
        }
    }

    private IReadOnlyCollection<int> GetResultsUsingListAsReadOnlyCollection()
    {
        var results = new List<int>();
        foreach (var item in _dataset)
        {
            results.Add(item);
        }

        return results;
    }

    private List<int> GetResultsUsingListAsList()
    {
        var results = new List<int>();
        foreach (var item in _dataset)
        {
            results.Add(item);
        }

        return results;
    }

    private IEnumerable<int> GetResultsUsingIterator()
    {
        foreach (var item in _dataset)
        {
            yield return item;
        }
    }

    [Benchmark]
    public void LinqAny()
    {
        _ = Implementation switch
        {
            ImplementationScenario.Iterator => GetResultsUsingIterator().Any(),
            ImplementationScenario.PopulateAsList => GetResultsUsingListAsList().Any(),
            ImplementationScenario.PopulateAsROCollection => GetResultsUsingListAsReadOnlyCollection().Any(),
            _ => throw new NotImplementedException(Implementation.ToString()),
        };
    }

    [Benchmark]
    public void LinqCount()
    {
        _ = Implementation switch
        {
            ImplementationScenario.Iterator => GetResultsUsingIterator().Count(),
            ImplementationScenario.PopulateAsList => GetResultsUsingListAsList().Count(),
            ImplementationScenario.PopulateAsROCollection => GetResultsUsingListAsReadOnlyCollection().Count(),
            _ => throw new NotImplementedException(Implementation.ToString()),
        };
    }

    [Benchmark]
    public void LinqToArray()
    {
        _ = Implementation switch
        {
            ImplementationScenario.Iterator => GetResultsUsingIterator().ToArray(),
            ImplementationScenario.PopulateAsList => GetResultsUsingListAsList().ToArray(),
            ImplementationScenario.PopulateAsROCollection => GetResultsUsingListAsReadOnlyCollection().ToArray(),
            _ => throw new NotImplementedException(Implementation.ToString()),
        };
    }

    [Benchmark]
    public void LinqTakeHalfToArray()
    {
        _ = Implementation switch
        {
            ImplementationScenario.Iterator => GetResultsUsingIterator().Take(_dataset.Length / 2).ToArray(),
            ImplementationScenario.PopulateAsList => GetResultsUsingListAsList().Take(_dataset.Length / 2).ToArray(),
            ImplementationScenario.PopulateAsROCollection => GetResultsUsingListAsReadOnlyCollection().Take(_dataset.Length / 2).ToArray(),
            _ => throw new NotImplementedException(Implementation.ToString()),
        };
    }

    [Benchmark]
    public void Foreach()
    {
        switch (Implementation)
        {
            case ImplementationScenario.Iterator:
                foreach (var x in GetResultsUsingIterator())
                {
                }
                break;
            case ImplementationScenario.PopulateAsList:
                foreach (var x in GetResultsUsingListAsList())
                {
                }
                break;
            case ImplementationScenario.PopulateAsROCollection:
                foreach (var x in GetResultsUsingListAsReadOnlyCollection())
                {
                }
                break;
            default:
                throw new NotImplementedException(Implementation.ToString());
        }
    }
}