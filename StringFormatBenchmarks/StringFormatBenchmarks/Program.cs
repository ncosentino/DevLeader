// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Text;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

BenchmarkSwitcher switcher = new(Assembly.GetExecutingAssembly());
switcher.Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class SimpleBenchmarks
{
    private readonly string _parameter;
    private readonly string _prefix;
    private readonly string _suffix;

    [Params(1, 1_000, 1_000_000)]
    public int ParamerterLength;

    [Params(1, 1_000, 1_000_000)]
    public int PrefixLength;

    [Params(1, 1_000, 1_000_000)]
    public int SuffixLength;

    public SimpleBenchmarks()
    {
        _parameter = new string('a', ParamerterLength);
        _prefix = new string('b', PrefixLength);
        _suffix = new string('c', SuffixLength);
    }

    [Benchmark]
    public string Variable_StringFormat()
    {
        return string.Format(
            "{0}{1}{2}",
            _prefix,
            _parameter,
            _suffix);
    }

    [Benchmark]
    public string Variable_StringInterpolation()
    {
        return $"{_prefix}{_parameter}{_suffix}";
    }

    [Benchmark]
    public string Variable_StringConcatenation()
    {
        return _prefix + _parameter + _suffix;
    }
}

[ShortRunJob]
[MemoryDiagnoser]
public class AppendInLoopBenchmarks
{
    private readonly string _parameter;

    [Params(1, 1_000, 1_000_000)]
    public int ParamerterLength;

    [Params(1, 10, 100)]
    public int Iterations;

    public AppendInLoopBenchmarks()
    {
        _parameter = new string('a', ParamerterLength);
    }

    [Benchmark]
    public string StringFormat()
    {
        string result = "";
        for (int i = 0; i < Iterations; i++)
        {
            result = string.Format(
                "{0}{1}",
                result,
                _parameter);
        }

        return result;
    }

    [Benchmark]
    public string StringInterpolation()
    {
        string result = "";
        for (int i = 0; i < Iterations; i++)
        {
            result = $"{result}{_parameter}";
        }

        return result;
    }

    [Benchmark]
    public void StringConcatenation()
    {
        string result = "";
        for (int i = 0; i < Iterations; i++)
        {
            result = result + _parameter;
        }
    }

    [Benchmark]
    public string StringBuilder()
    {
        StringBuilder result = new();
        for (int i = 0; i < Iterations; i++)
        {
            result.Append(_parameter);
        }

        return result.ToString();
    }
}