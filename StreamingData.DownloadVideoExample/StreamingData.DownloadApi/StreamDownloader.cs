using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics.CodeAnalysis;

var serviceCollection = new ServiceCollection();
serviceCollection.AddHttpClient();
serviceCollection.AddSingleton<StreamDownloader>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var url = "https://hil-speed.hetzner.com/10GB.bin";
var streamDownloader = serviceProvider.GetRequiredService<StreamDownloader>();

var streamResult = await streamDownloader.GetStreamAsync(url);
SomeOtherSystemComponent component = new();

if (!streamResult.Stream.TryAsStreamWithLength(out var streamWithLength))
{
    // FIXME: we can't continue without the length!
    return;
}

component.DoSomething(streamWithLength);

//component.DoSomething(new StreamWithLength(
//    streamResult.Stream,
//    streamResult.HasLength
//        ? streamResult.Stream.Length
//        : throw new InvalidOperationException("We need a length!"),
//    true));

class SomeOtherSystemComponent
{
    public void DoSomething(StreamWithLength stream)
    {
        // do things...
        Console.WriteLine($"Length: {stream.Length}");
    }
}

/*
record StreamResult(
    Stream Stream,
    long? Length);

class StreamDownloader
{
    private readonly IHttpClientFactory _httpClientFactory;

    public StreamDownloader(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<StreamResult> GetStreamAsync(
        string url,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient();

        var response = await client
            .GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        var stream = await response.Content
            .ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);

        if (TryGetContentLength(response, out var contentLength))
        {
            return new(stream, Length: contentLength);
        }

        return new(
            Stream: stream,
            Length: stream.CanSeek ? stream.Length : null);
    }

    private static bool TryGetContentLength(
        HttpResponseMessage response,
        out long length)
    {
        var hasLengthHeader = response.Content.Headers.TryGetValues(
            "Content-Length",
            out var contentLengthHeaders);
        var rawLength = hasLengthHeader
            ? contentLengthHeaders.First()
            : "Unknown";
        return long.TryParse(rawLength, out length);
    }
}
*/

///*
public record StreamResult(
    Stream Stream,
    bool HasLength);

public class StreamDownloader
{
    private readonly IHttpClientFactory _httpClientFactory;

    public StreamDownloader(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<StreamResult> GetStreamAsync(
        string url,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient();

        var response = await client
            .GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        var stream = await response.Content
            .ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);

        if (TryGetContentLength(response, out var contentLength))
        {
            StreamWithLength streamWithLength = new(
                stream,
                contentLength,
                takeOwnership: true);
            return new(streamWithLength, HasLength: true);
        }

        return new(
            Stream: stream, 
            HasLength: stream.CanSeek);
    }

    private static bool TryGetContentLength(
        HttpResponseMessage response,
        out long length)
    {
        var hasLengthHeader = response.Content.Headers.TryGetValues(
            "Content-Length",
            out var contentLengthHeaders);
        var rawLength = hasLengthHeader
            ? contentLengthHeaders.First()
            : "Unknown";
        return long.TryParse(rawLength, out length);
    }
}
//*/

public class StreamWithLength : Stream
{
    private readonly Stream _inner;
    private readonly bool _takeOwnership;

    public StreamWithLength(
        Stream inner,
        long length,
        bool takeOwnership)
    {
        _inner = inner;
        Length = length;
        _takeOwnership = takeOwnership;
    }

    public override bool CanRead => _inner.CanRead;

    public override bool CanSeek => _inner.CanSeek;

    public override bool CanWrite => _inner.CanWrite;

    public override long Length { get; }

    public override long Position { get => _inner.Position; set => _inner.Position = value; }

    public override void Flush() => _inner.Flush();

    public override int Read(byte[] buffer, int offset, int count)
        => _inner.Read(buffer, offset, count);

    public override long Seek(long offset, SeekOrigin origin)
        => _inner.Seek(offset, origin);

    public override void SetLength(long value)
        => throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count)
        => _inner.Write(buffer, offset, count);

    protected override void Dispose(bool disposing)
    {
        if (disposing && _takeOwnership)
        {
            _inner.Dispose();
        }

        base.Dispose(disposing);
    }
}

public static class StreamExtensions
{
    public static bool TryAsStreamWithLength(
        this Stream stream,
        // Thanks to YouTube comments for
        // suggesting this attribute!
        [NotNullWhen(true)] out StreamWithLength? streamWithLength)
    {
        if (stream is StreamWithLength casted)
        {
            streamWithLength = casted;
            return true;
        }

        try
        {
            if (stream.CanSeek)
            {
                streamWithLength = new(
                    stream,
                    stream.Length,
                    takeOwnership: true);
                return true;
            }
        }
        catch
        {
            // cannot make stream with length
        }

        streamWithLength = null;
        return false;
    }
}