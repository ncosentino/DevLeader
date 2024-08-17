using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

BenchmarkRunner.Run(Assembly.GetExecutingAssembly());

[MemoryDiagnoser]
[ShortRunJob]
public class Benchmarks
{
    private const string url = "https://download.samplelib.com/mp4/sample-30s.mp4";

    private StreamDownloader? _streamDownloader;
    private IHttpClientFactory? _httpClientFactory;

    [GlobalSetup]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddHttpClient();
        serviceCollection.AddSingleton<StreamDownloader>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _streamDownloader = serviceProvider.GetRequiredService<StreamDownloader>();
        _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    }

    [Benchmark]
    public async Task NetworkStream_ToNullStream()
    {
        var streamResult = await _streamDownloader!.GetStreamAsync(url);

        if (!streamResult.Stream.TryAsStreamWithLength(out var streamWithLength))
        {
            throw new InvalidOperationException();
        }

        await streamWithLength.CopyToAsync(Stream.Null);
    }

    [Benchmark]
    public async Task MemoryStream_ToNullStream()
    {
        var client = _httpClientFactory!.CreateClient();
        var response = await client
            .GetAsync(url, HttpCompletionOption.ResponseContentRead, default)
            .ConfigureAwait(false);
        var stream = await response.Content
            .ReadAsStreamAsync(default)
            .ConfigureAwait(false);

        await stream.CopyToAsync(Stream.Null);
    }

    [Benchmark]
    public async Task NetworkStream_ToFileStream()
    {
        var streamResult = await _streamDownloader!.GetStreamAsync(url);

        if (!streamResult.Stream.TryAsStreamWithLength(out var streamWithLength))
        {
            throw new InvalidOperationException();
        }

        using var fileStream = new FileStream(
            "sample.mp4",
            FileMode.OpenOrCreate, 
            FileAccess.Write, 
            FileShare.None);
        await streamWithLength.CopyToAsync(fileStream);
    }

    [Benchmark]
    public async Task MemoryStream_ToFileStream()
    {
        var client = _httpClientFactory!.CreateClient();
        var response = await client
            .GetAsync(url, HttpCompletionOption.ResponseContentRead, default)
            .ConfigureAwait(false);
        var stream = await response.Content
            .ReadAsStreamAsync(default)
            .ConfigureAwait(false);

        using var fileStream = new FileStream(
            "sample2.mp4",
            FileMode.OpenOrCreate,
            FileAccess.Write,
            FileShare.None);
        await stream.CopyToAsync(fileStream);
    }
}