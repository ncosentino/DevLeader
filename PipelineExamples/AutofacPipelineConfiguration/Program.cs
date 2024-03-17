using Autofac;

ContainerBuilder containerBuilder = new();
containerBuilder.RegisterModule<PipelineModule>();

using var container = containerBuilder.Build();
using var lifetimeScope = container.BeginLifetimeScope();

var pipeline = lifetimeScope.Resolve<Pipeline>();
pipeline.Execute();

public sealed class PipelineModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<PipelineSource>()
            .SingleInstance();
        builder
            .RegisterType<TextCleaner>()
            .SingleInstance();
        builder
            .RegisterType<WordCounter>()
            .SingleInstance();
        builder
            .RegisterType<TextSummarizer>()
            .SingleInstance();
        builder
            .RegisterType<PipelineSink>()
            .SingleInstance();
        builder
            .RegisterType<Pipeline>()
            .SingleInstance();
    }
}

public sealed class Pipeline(
    PipelineSource _source,
    TextCleaner _cleaner,
    WordCounter _counter,
    TextSummarizer _summarizer,
    PipelineSink _sink)
{
    public void Execute()
    {
        var input = _source.Start();
        var cleanedText = _cleaner.Clean(input);
        var wordFrequency = _counter.CountWords(cleanedText);
        var summary = _summarizer.Summarize(wordFrequency);
        _sink.Consume(summary);
    }
}

public sealed class PipelineSink
{
    public void Consume(SummarizationResult result)
    {
        Console.WriteLine(result.Summary);
    }
}

public sealed class PipelineSource
{
    public string Start()
    {
        Console.WriteLine("Enter text to feed into the pipeline:");
        var input = Console.ReadLine()!;
        return input;
    }
}

public sealed class TextCleaner
{
    public CleanResult Clean(string input)
    {
        // Remove punctuation and
        // convert to lower case
        var cleanedText = new string(input
            .Where(c => !char.IsPunctuation(c))
            .ToArray());
        return new CleanResult(cleanedText.ToLower());
    }
}

public sealed class WordCounter
{
    public FrequencyResult CountWords(CleanResult cleanResult)
    {
        var wordFrequency = new Dictionary<string, int>();
        var words = cleanResult.CleanedText.Split(' ');

        foreach (var word in words)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                continue;
            }

            if (wordFrequency.TryGetValue(
                word,
                out int value))
            {
                wordFrequency[word] = ++value;
            }
            else
            {
                wordFrequency[word] = 1;
            }
        }

        return new FrequencyResult(wordFrequency);
    }
}

public sealed class TextSummarizer
{
    public SummarizationResult Summarize(
        FrequencyResult frequencyResult)
    {
        // Summarize by picking top
        // 3 frequent words
        var topWords = frequencyResult
            .WordFrequency
            .OrderByDescending(kvp => kvp.Value)
            .Take(3)
            .Select(kvp => kvp.Key);
        var formatted =
            $"Top words: " +
            $"{string.Join(", ", topWords)}";
        return new SummarizationResult(formatted);
    }
}

public sealed record CleanResult(
    string CleanedText);

public sealed record FrequencyResult(
    IReadOnlyDictionary<string, int> WordFrequency);

public sealed record SummarizationResult(
    string Summary);




public interface IPipeline
{
    public void Execute();
}

public interface IPipelineStage
{
}

public interface IPipelineIntermediate<TInput, TOutput> : IPipelineStage
{
    TOutput Execute(TInput input);
}

public interface IPipelineSource : IPipelineStage
{
}

public interface IPipelineSource<TOutput> : IPipelineSource
{
    TOutput Start();
}

public interface IPipelineSink : IPipelineStage
{
}

public interface IPipelineSink<TInput> : IPipelineSink
{
    void Consume(TInput input);
}

public sealed class Source : IPipelineSource<string>
{
    public string Start()
    {
        Console.WriteLine("Enter text to feed into the pipeline:");
        var input = Console.ReadLine()!;
        return input;
    }
}

public sealed class Sink : IPipelineSink<string>
{
    public void Consume(string input)
    {
        Console.WriteLine(input);
    }
}

public sealed class MostlyAutoPipeline(
    IPipelineSource<string> _source,
    IReadOnlyList<IPipelineIntermediate<string, string>> _stages,
    IPipelineSink<string> _sink) :
    IPipeline
{
    public void Execute()
    {
        var current = _source.Start();

        foreach (var stage in _stages)
        {
            current = stage.Execute(current);
        }
        
        _sink.Consume(current);
    }
}







public interface IPrioritizedPipelineIntermediate<TInput, TOutput> :
    IPipelineIntermediate<TInput, TOutput>
{
    public uint Priority { get; }
}

public sealed class MostlyAutoPipelineBuilder(
    IPipelineSource<string> _source,
    IReadOnlyList<IPrioritizedPipelineIntermediate<string, string>> _stages,
    IPipelineSink<string> _sink)
{
    public IPipeline Build()
    {
        MostlyAutoPipeline pipeline = new(
            _source,
            _stages.OrderBy(x => x.Priority).ToArray(),
            _sink);
        return pipeline;
    }
}
