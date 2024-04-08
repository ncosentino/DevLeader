using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using System.Reflection;
using System.Text.RegularExpressions;

BenchmarkRunner.Run(
    Assembly.GetExecutingAssembly(), 
    args: args);

[MemoryDiagnoser]
[MediumRunJob]
public partial class RegexBenchmarks
{
    private const string RegexPattern = @"\b\w*(ing|ed)\b";

    private string? _sourceText;
    private Regex? _regex;
    private Regex? _regexCompiled;
    private Regex? _generatedRegex;
    private Regex? _generatedRegexCompiled;

    [GeneratedRegex(RegexPattern, RegexOptions.None, "en-US")]
    private static partial Regex GetGeneratedRegex();

    [GeneratedRegex(RegexPattern, RegexOptions.Compiled, "en-US")]
    private static partial Regex GetGeneratedRegexCompiled();

    [Params("pg73346.txt")]
    public string? SourceFileName { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _sourceText = File.ReadAllText(SourceFileName!);
        
        _regex = new(RegexPattern);
        _regexCompiled = new(RegexPattern, RegexOptions.Compiled);
        _generatedRegex = GetGeneratedRegex();
        _generatedRegexCompiled = GetGeneratedRegexCompiled();
    }

    [Benchmark(Baseline = true)]
    public MatchCollection Static()
    {
        return Regex.Matches(_sourceText!, RegexPattern!);
    }

    [Benchmark]
    public MatchCollection New()
    {
        Regex regex = new(RegexPattern!);
        return regex.Matches(_sourceText!);
    }

    [Benchmark]
    public MatchCollection New_Compiled()
    {
        Regex regex = new(RegexPattern!, RegexOptions.Compiled);
        return regex.Matches(_sourceText!);
    }

    [Benchmark]
    public MatchCollection Cached()
    {
        return _regex!.Matches(_sourceText!);
    }

    [Benchmark]
    public MatchCollection Cached_Compiled()
    {
        return _regexCompiled!.Matches(_sourceText!);
    }

    [Benchmark]
    public MatchCollection Generated()
    {
        return GetGeneratedRegex().Matches(_sourceText!);
    }

    [Benchmark]
    public MatchCollection Generated_Cached()
    {
        return _generatedRegex!.Matches(_sourceText!);
    }

    [Benchmark]
    public MatchCollection Generated_Compiled()
    {
        return GetGeneratedRegexCompiled().Matches(_sourceText!);
    }

    [Benchmark]
    public MatchCollection Generated_Cached_Compiled()
    {
        return _generatedRegexCompiled!.Matches(_sourceText!);
    }
}